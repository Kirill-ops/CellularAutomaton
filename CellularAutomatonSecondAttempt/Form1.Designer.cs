namespace CellularAutomatonSecondAttempt
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
            pBox = new PictureBox();
            buttonRun = new Button();
            buttonClear = new Button();
            ((System.ComponentModel.ISupportInitialize)pBox).BeginInit();
            SuspendLayout();
            // 
            // pBox
            // 
            pBox.BackColor = Color.White;
            pBox.Location = new Point(12, 12);
            pBox.Name = "pBox";
            pBox.Size = new Size(1196, 754);
            pBox.TabIndex = 0;
            pBox.TabStop = false;
            pBox.MouseClick += pBox_MouseClick;
            // 
            // buttonRun
            // 
            buttonRun.Location = new Point(12, 772);
            buttonRun.Name = "buttonRun";
            buttonRun.Size = new Size(112, 34);
            buttonRun.TabIndex = 1;
            buttonRun.Text = "ПоХнали";
            buttonRun.UseVisualStyleBackColor = true;
            buttonRun.Click += buttonRun_Click;
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(929, 772);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(279, 34);
            buttonClear.TabIndex = 4;
            buttonClear.Text = "Чичтим Чистим шорх шорх";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 192);
            ClientSize = new Size(1229, 818);
            Controls.Add(buttonClear);
            Controls.Add(buttonRun);
            Controls.Add(pBox);
            Name = "Form1";
            Text = "Кубики УААА";
            ((System.ComponentModel.ISupportInitialize)pBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pBox;
        private Button buttonRun;
        private Button buttonClear;
    }
}