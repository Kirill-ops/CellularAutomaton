namespace CellularAutomaton
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pBox).BeginInit();
            SuspendLayout();
            // 
            // pBox
            // 
            pBox.BackColor = Color.White;
            pBox.Location = new Point(12, 12);
            pBox.Name = "pBox";
            pBox.Size = new Size(1186, 566);
            pBox.TabIndex = 0;
            pBox.TabStop = false;
            pBox.MouseClick += pBox_MouseClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(1210, 590);
            Controls.Add(pBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pBox;
    }
}