using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DevicesTest
{
    public sealed class DeviceHelper
    {
        private DeviceHelper()
        {
            // Non
        }

        public static void SetDeviceEnabled(Guid classGuid, string instanceId, bool enable)
        {
            SafeDeviceInfoSetHandle diSetHandle = null;
            try
            {
                // Get the handle to a device information set for all devices matching classGuid that are present on the 
                // system.
                diSetHandle = Win32.SetupDiGetClassDevs(ref classGuid, null, IntPtr.Zero, Win32.SetupDiGetClassDevsFlags.Present);
                // Get the device information data for each matching device.
                Win32.DeviceInfoData[] diData = GetDeviceInfoData(diSetHandle);
                // Find the index of our instance. i.e. the touchpad mouse - I have 3 mice attached...
                int index = GetIndexOfInstance(diSetHandle, diData, instanceId);
                // Disable...
                EnableDevice(diSetHandle, diData[index], enable);
            }
            finally
            {
                if (diSetHandle != null)
                {
                    if (diSetHandle.IsClosed == false)
                    {
                        diSetHandle.Close();
                    }
                    diSetHandle.Dispose();
                }
            }
        }

        private static Win32.DeviceInfoData[] GetDeviceInfoData(SafeDeviceInfoSetHandle handle)
        {
            List<Win32.DeviceInfoData> data = new List<Win32.DeviceInfoData>();
            Win32.DeviceInfoData did = new Win32.DeviceInfoData();
            int didSize = Marshal.SizeOf(did);
            did.Size = didSize;
            int index = 0;
            while (Win32.SetupDiEnumDeviceInfo(handle, index, ref did))
            {
                data.Add(did);
                index += 1;
                did = new Win32.DeviceInfoData();
                did.Size = didSize;
            }
            return data.ToArray();
        }

        // Find the index of the particular DeviceInfoData for the instanceId.
        private static int GetIndexOfInstance(SafeDeviceInfoSetHandle handle, Win32.DeviceInfoData[] diData, string instanceId)
        {
            const int ERROR_INSUFFICIENT_BUFFER = 122;
            for (int index = 0; index <= diData.Length - 1; index++)
            {
                StringBuilder sb = new StringBuilder(1);
                int requiredSize = 0;
                bool result = Win32.SetupDiGetDeviceInstanceId(handle.DangerousGetHandle(), ref diData[index], sb, sb.Capacity, out requiredSize);
                if (result == false && Marshal.GetLastWin32Error() == ERROR_INSUFFICIENT_BUFFER)
                {
                    sb.Capacity = requiredSize;
                    result = Win32.SetupDiGetDeviceInstanceId(handle.DangerousGetHandle(), ref diData[index], sb, sb.Capacity, out requiredSize);
                }
                if (result == false)
                    throw new Win32Exception();
                if (instanceId.Equals(sb.ToString()))
                {
                    return index;
                }
            }
            // not found
            return -1;
        }

        // enable/disable...
        private static void EnableDevice(SafeDeviceInfoSetHandle handle, Win32.DeviceInfoData diData, bool enable)
        {
            Win32.PropertyChangeParameters @params = new Win32.PropertyChangeParameters();
            // The size is just the size of the header, but we've flattened the structure.
            // The header comprises the first two fields, both integer.
            @params.Size = 8;
            @params.DiFunction = Win32.DiFunction.PropertyChange;
            @params.Scope = Win32.Scopes.Global;
            if (enable)
            {
                @params.StateChange = Win32.StateChangeAction.Enable;
            }
            else
            {
                @params.StateChange = Win32.StateChangeAction.Disable;
            }

            bool result = Win32.SetupDiSetClassInstallParams(handle, ref diData, ref @params, Marshal.SizeOf(@params));
            if (result == false) throw new Win32Exception();
            result = Win32.SetupDiCallClassInstaller(Win32.DiFunction.PropertyChange, handle, ref diData);
            if (result == false)
            {
                int err = Marshal.GetLastWin32Error();
                if (err == (int)Win32.SetupApiError.NotDisableable)
                    throw new ArgumentException("Device can't be disabled (programmatically or in Device Manager).");
                else if (err >= (int)Win32.SetupApiError.NoAssociatedClass && err <= (int)Win32.SetupApiError.OnlyValidateViaAuthenticode)
                    throw new Win32Exception("SetupAPI error: " + ((Win32.SetupApiError)err).ToString());
                else
                    throw new Win32Exception();
            }
        }
    }
}
