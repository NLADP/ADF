        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gpButtons = new System.Windows.Forms.GroupBox();
            
<Tobago.Loop(UseCase.Attributes, "InitializeControls")>

            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.gpButtons.SuspendLayout();
            this.flpMain.SuspendLayout();
            this.flpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpButtons
            // 
            this.gpButtons.AutoSize = true;
            this.gpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpButtons.Controls.Add(this.flpButtons);
            this.gpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpButtons.Location = new System.Drawing.Point(10, 182);
            this.gpButtons.Margin = new System.Windows.Forms.Padding(10);
            this.gpButtons.Name = "gpButtons";
            this.gpButtons.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.gpButtons.Size = new System.Drawing.Size(277, 46);
            this.gpButtons.TabIndex = 3;
            this.gpButtons.TabStop = false;
            // 
            // flpMain
            // 
            this.flpMain.AutoSize = true;
            this.flpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMain.Location = new System.Drawing.Point(0, 0);
            this.flpMain.Name = "flpMain";
            this.flpMain.Size = new System.Drawing.Size(297, 224);
            this.flpMain.TabIndex = 4;
<Tobago.Loop(UseCase.Attributes, "SetControls")>            
            
            // 
            // flpButtons
            // 
            this.flpButtons.AutoSize = true;
            this.flpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpButtons.Location = new System.Drawing.Point(16, 19);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.Size = new System.Drawing.Size(243, 39);
            this.flpButtons.TabIndex = 2;
            this.flpButtons.WrapContents = false;
            // 
            // $UseCase.Name.Pascal$
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(604, 545);
            this.Controls.Add(this.flpMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = "$UseCase.Name$";
            this.Padding = new System.Windows.Forms.Padding(10);
<Tobago.Loop(UseCase.Attributes, "FinalizeControls")>
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
            this.flpButtons.ResumeLayout(false);
            this.flpButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

<Tobago.Loop(UseCase.Attributes, "DeclareControls")>
	private GroupBox gpButtons;
	private FlowLayoutPanel flpMain;
	private FlowLayoutPanel flpButtons;