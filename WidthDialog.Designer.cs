namespace RudimentaryGameEngine
{
	partial class WidthDialog
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
			this.NUPWidth = new System.Windows.Forms.NumericUpDown();
			this.lblWidth = new System.Windows.Forms.Label();
			this.btnDone = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.NUPWidth)).BeginInit();
			this.SuspendLayout();
			// 
			// NUPWidth
			// 
			this.NUPWidth.Location = new System.Drawing.Point(53, 12);
			this.NUPWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.NUPWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NUPWidth.Name = "NUPWidth";
			this.NUPWidth.Size = new System.Drawing.Size(119, 20);
			this.NUPWidth.TabIndex = 0;
			this.NUPWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NUPWidth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.NUPWidth_KeyUp);
			// 
			// lblWidth
			// 
			this.lblWidth.AutoSize = true;
			this.lblWidth.Location = new System.Drawing.Point(12, 14);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(38, 13);
			this.lblWidth.TabIndex = 1;
			this.lblWidth.Text = "Width:";
			// 
			// btnDone
			// 
			this.btnDone.Location = new System.Drawing.Point(178, 9);
			this.btnDone.Name = "btnDone";
			this.btnDone.Size = new System.Drawing.Size(52, 23);
			this.btnDone.TabIndex = 2;
			this.btnDone.Text = "Done";
			this.btnDone.UseVisualStyleBackColor = true;
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// WidthDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(236, 46);
			this.Controls.Add(this.btnDone);
			this.Controls.Add(this.lblWidth);
			this.Controls.Add(this.NUPWidth);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "WidthDialog";
			this.Text = "WidthDialog";
			((System.ComponentModel.ISupportInitialize)(this.NUPWidth)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown NUPWidth;
		private System.Windows.Forms.Label lblWidth;
		private System.Windows.Forms.Button btnDone;
	}
}