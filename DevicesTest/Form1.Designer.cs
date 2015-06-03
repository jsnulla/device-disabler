namespace DevicesTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.enableBtn = new System.Windows.Forms.Button();
            this.disableBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // enableBtn
            // 
            this.enableBtn.Location = new System.Drawing.Point(12, 12);
            this.enableBtn.Name = "enableBtn";
            this.enableBtn.Size = new System.Drawing.Size(229, 23);
            this.enableBtn.TabIndex = 1;
            this.enableBtn.Text = "Enable Kinect Camera";
            this.enableBtn.UseVisualStyleBackColor = true;
            this.enableBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // disableBtn
            // 
            this.disableBtn.Location = new System.Drawing.Point(12, 41);
            this.disableBtn.Name = "disableBtn";
            this.disableBtn.Size = new System.Drawing.Size(229, 23);
            this.disableBtn.TabIndex = 2;
            this.disableBtn.Text = "Disable Kinect Camera";
            this.disableBtn.UseVisualStyleBackColor = true;
            this.disableBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 74);
            this.Controls.Add(this.disableBtn);
            this.Controls.Add(this.enableBtn);
            this.Name = "Form1";
            this.Text = "Device Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button enableBtn;
        private System.Windows.Forms.Button disableBtn;
    }
}

