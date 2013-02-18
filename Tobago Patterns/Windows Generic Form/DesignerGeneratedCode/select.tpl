        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNew = new System.Windows.Forms.Button();
            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.flpButtonsLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.gpButtons = new System.Windows.Forms.GroupBox();
            this.flpButtonsRight = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
<Tobago.Loop(UseCase.Attributes, "InitializeControls")>                        
           
            this.gpButtons.SuspendLayout();
            this.flpMain.SuspendLayout();
            this.flpButtonsRight.SuspendLayout();
            this.flpButtonsLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpMain
            // 
            this.flpMain.AutoSize = true;
            this.flpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpMain.Controls.Add(this.gpButtons);
            this.flpMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpMain.Location = new System.Drawing.Point(0, 0);
            this.flpMain.Name = "flpMain";
            this.flpMain.Size = new System.Drawing.Size(753, 300);
            this.flpMain.TabIndex = 5;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "bNew";
            this.btnNew.Size = new System.Drawing.Size(75, 33);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
<Tobago.Loop(UseCase.Attributes, "SetControls")>


            
            
            
            // 
            // gpButtons
            // 
            this.gpButtons.AutoSize = true;
            this.gpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gpButtons.Controls.Add(this.flpButtonsRight);
            this.gpButtons.Controls.Add(this.flpButtonsLeft);
            this.gpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpButtons.Location = new System.Drawing.Point(10, 230);
            this.gpButtons.Margin = new System.Windows.Forms.Padding(10);
            this.gpButtons.Name = "gpButtons";
            this.gpButtons.Padding = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.gpButtons.Size = new System.Drawing.Size(733, 60);
            this.gpButtons.TabIndex = 1;
            this.gpButtons.TabStop = false;
            // 
            // flpButtonsRight
            // 
            this.flpButtonsRight.AutoSize = true;
            this.flpButtonsRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpButtonsRight.Controls.Add(this.btnCancel);
            this.flpButtonsRight.Controls.Add(this.btnOk);
            this.flpButtonsRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpButtonsRight.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpButtonsRight.Location = new System.Drawing.Point(84, 16);
            this.flpButtonsRight.Name = "flpButtonsRight";
            this.flpButtonsRight.Size = new System.Drawing.Size(646, 39);
            this.flpButtonsRight.TabIndex = 1;
            // 
            // bCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(568, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 33);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // bOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(487, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 33);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // flpButtonsLeft
            // 
            this.flpButtonsLeft.AutoSize = true;
            this.flpButtonsLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpButtonsLeft.Controls.Add(this.btnNew);
            this.flpButtonsLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpButtonsLeft.Location = new System.Drawing.Point(3, 16);
            this.flpButtonsLeft.Name = "flpButtonsLeft";
            this.flpButtonsLeft.Size = new System.Drawing.Size(81, 39);
            this.flpButtonsLeft.TabIndex = 0;
            // 
            // $UseCase.Name.Pascal$
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(769, 370);
            this.Controls.Add(this.flpMain);
            this.Name = "$UseCase.Name.Pascal$";
            this.ShowAsModal = true;
            this.Text = "$UseCase.Name$";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.$UseCase.Name.Pascal$_FormClosing);
            this.Load += new System.EventHandler(this.$UseCase.Name.Pascal$_Load);
            this.flpMain.ResumeLayout(false);
            this.flpMain.PerformLayout();
<Tobago.Loop(UseCase.Attributes, "FinalizeControls")>            
            
            this.gpButtons.ResumeLayout(false);
            this.gpButtons.PerformLayout();
            this.flpButtonsRight.ResumeLayout(false);
            this.flpButtonsLeft.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

	<Tobago.Loop(UseCase.Attributes, "DeclareControls")>
        private Button btnNew;
        private FlowLayoutPanel flpMain;
        private Button btnOk;
        private Button btnCancel;
        private GroupBox gpButtons;
        private FlowLayoutPanel flpButtonsLeft;
        private FlowLayoutPanel flpButtonsRight;

