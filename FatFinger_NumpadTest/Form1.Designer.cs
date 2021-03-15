namespace FatFinger_NumpadTest {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent () {
			FatFinger_NumpadTest.KeyMap keyMap1 = new FatFinger_NumpadTest.KeyMap();
			this.shiftCheck = new System.Windows.Forms.CheckBox();
			this.alphaRadio = new System.Windows.Forms.RadioButton();
			this.numericRadio = new System.Windows.Forms.RadioButton();
			this.accentsRadio = new System.Windows.Forms.RadioButton();
			this.symbolsRadio = new System.Windows.Forms.RadioButton();
			this.ctrlCheck = new System.Windows.Forms.CheckBox();
			this.altCheck = new System.Windows.Forms.CheckBox();
			this.keyHintView = new FatFinger_NumpadTest.KeyHintView();
			this.SuspendLayout();
			// 
			// shiftCheck
			// 
			this.shiftCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.shiftCheck.Location = new System.Drawing.Point(0, 178);
			this.shiftCheck.Name = "shiftCheck";
			this.shiftCheck.Size = new System.Drawing.Size(47, 19);
			this.shiftCheck.TabIndex = 2;
			this.shiftCheck.Text = "Shift";
			this.shiftCheck.UseVisualStyleBackColor = false;
			this.shiftCheck.CheckStateChanged += new System.EventHandler(this.UpdateModifiersFromChecks);
			// 
			// alphaRadio
			// 
			this.alphaRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.alphaRadio.Appearance = System.Windows.Forms.Appearance.Button;
			this.alphaRadio.Checked = true;
			this.alphaRadio.Location = new System.Drawing.Point(54, 179);
			this.alphaRadio.Name = "alphaRadio";
			this.alphaRadio.Size = new System.Drawing.Size(42, 23);
			this.alphaRadio.TabIndex = 3;
			this.alphaRadio.TabStop = true;
			this.alphaRadio.Tag = "tg";
			this.alphaRadio.Text = "Abc";
			this.alphaRadio.UseVisualStyleBackColor = true;
			this.alphaRadio.CheckedChanged += new System.EventHandler(this.ModeChange);
			// 
			// numericRadio
			// 
			this.numericRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.numericRadio.Appearance = System.Windows.Forms.Appearance.Button;
			this.numericRadio.Location = new System.Drawing.Point(95, 179);
			this.numericRadio.Name = "numericRadio";
			this.numericRadio.Size = new System.Drawing.Size(37, 23);
			this.numericRadio.TabIndex = 4;
			this.numericRadio.Tag = "tg";
			this.numericRadio.Text = "123";
			this.numericRadio.UseVisualStyleBackColor = true;
			this.numericRadio.CheckedChanged += new System.EventHandler(this.ModeChange);
			// 
			// accentsRadio
			// 
			this.accentsRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.accentsRadio.Appearance = System.Windows.Forms.Appearance.Button;
			this.accentsRadio.Location = new System.Drawing.Point(131, 179);
			this.accentsRadio.Name = "accentsRadio";
			this.accentsRadio.Size = new System.Drawing.Size(54, 23);
			this.accentsRadio.TabIndex = 5;
			this.accentsRadio.Tag = "tg";
			this.accentsRadio.Text = "Accents";
			this.accentsRadio.UseVisualStyleBackColor = true;
			this.accentsRadio.CheckedChanged += new System.EventHandler(this.ModeChange);
			// 
			// symbolsRadio
			// 
			this.symbolsRadio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.symbolsRadio.Appearance = System.Windows.Forms.Appearance.Button;
			this.symbolsRadio.Location = new System.Drawing.Point(184, 179);
			this.symbolsRadio.Name = "symbolsRadio";
			this.symbolsRadio.Size = new System.Drawing.Size(56, 23);
			this.symbolsRadio.TabIndex = 6;
			this.symbolsRadio.Tag = "tg";
			this.symbolsRadio.Text = "Symbols";
			this.symbolsRadio.UseVisualStyleBackColor = true;
			this.symbolsRadio.CheckedChanged += new System.EventHandler(this.ModeChange);
			// 
			// ctrlCheck
			// 
			this.ctrlCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ctrlCheck.Location = new System.Drawing.Point(0, 192);
			this.ctrlCheck.Name = "ctrlCheck";
			this.ctrlCheck.Size = new System.Drawing.Size(47, 18);
			this.ctrlCheck.TabIndex = 7;
			this.ctrlCheck.Text = "Ctrl";
			this.ctrlCheck.UseVisualStyleBackColor = true;
			this.ctrlCheck.CheckStateChanged += new System.EventHandler(this.UpdateModifiersFromChecks);
			// 
			// altCheck
			// 
			this.altCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.altCheck.AutoSize = true;
			this.altCheck.Location = new System.Drawing.Point(0, 137);
			this.altCheck.Name = "altCheck";
			this.altCheck.Size = new System.Drawing.Size(38, 17);
			this.altCheck.TabIndex = 8;
			this.altCheck.Text = "Alt";
			this.altCheck.UseVisualStyleBackColor = true;
			this.altCheck.Visible = false;
			this.altCheck.CheckStateChanged += new System.EventHandler(this.UpdateModifiersFromChecks);
			// 
			// keyHintView
			// 
			this.keyHintView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.keyHintView.FirstDown = -1;
			this.keyHintView.LastUp = -1;
			keyMap1.CurrentMode = FatFinger_NumpadTest.KeyMap.MapMode.Alpha;
			this.keyHintView.LinkedKeyMap = keyMap1;
			this.keyHintView.Location = new System.Drawing.Point(0, 0);
			this.keyHintView.Name = "keyHintView";
			this.keyHintView.Size = new System.Drawing.Size(240, 179);
			this.keyHintView.TabIndex = 1;
			this.keyHintView.Text = "keyHintView1";
			this.keyHintView.Resize += new System.EventHandler(this.keyHintView_Resize);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(240, 207);
			this.Controls.Add(this.altCheck);
			this.Controls.Add(this.ctrlCheck);
			this.Controls.Add(this.symbolsRadio);
			this.Controls.Add(this.accentsRadio);
			this.Controls.Add(this.numericRadio);
			this.Controls.Add(this.alphaRadio);
			this.Controls.Add(this.shiftCheck);
			this.Controls.Add(this.keyHintView);
			this.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(226, 200);
			this.Name = "Form1";
			this.Opacity = 0.9;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Num Pad Type";
			this.TopMost = true;
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private KeyHintView keyHintView;
		private System.Windows.Forms.CheckBox shiftCheck;
		private System.Windows.Forms.RadioButton alphaRadio;
		private System.Windows.Forms.RadioButton numericRadio;
		private System.Windows.Forms.RadioButton accentsRadio;
		private System.Windows.Forms.RadioButton symbolsRadio;
		private System.Windows.Forms.CheckBox ctrlCheck;
		private System.Windows.Forms.CheckBox altCheck;
	}
}

