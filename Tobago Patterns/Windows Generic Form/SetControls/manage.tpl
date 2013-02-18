            this.flpMain.Controls.Add(this.gp$Attribute.Type$);           
            this.flpMain.Controls.Add(this.gpButtons);            
            // 
            // btnSelect$Attribute.Name$
            // 
            this.btnSelect$Attribute.Name$.AutoSize = true;
            this.btnSelect$Attribute.Name$.Location = new System.Drawing.Point(173, 3);
            this.btnSelect$Attribute.Name$.Name = "bSelect$Attribute.Name$";
            this.btnSelect$Attribute.Name$.Size = new System.Drawing.Size(75, 33);
            this.btnSelect$Attribute.Name$.TabIndex = 0;
            this.btnSelect$Attribute.Name$.Text = "Select";
            this.btnSelect$Attribute.Name$.UseVisualStyleBackColor = true;
            this.btnSelect$Attribute.Name$.Click += new System.EventHandler(this.btnSelect$Attribute.Name$_Click);
            // 
            // btnRemove$Attribute.Name$
            // 
            this.btnRemove$Attribute.Name$.AutoSize = true;
            this.btnRemove$Attribute.Name$.Location = new System.Drawing.Point(254, 3);
            this.btnRemove$Attribute.Name$.Name = "bRemove$Attribute.Name$";
            this.btnRemove$Attribute.Name$.Padding = new System.Windows.Forms.Padding(5);
            this.btnRemove$Attribute.Name$.Size = new System.Drawing.Size(75, 33);
            this.btnRemove$Attribute.Name$.TabIndex = 2;
            this.btnRemove$Attribute.Name$.Text = "Remove";
            this.btnRemove$Attribute.Name$.UseVisualStyleBackColor = true;
            this.btnRemove$Attribute.Name$.Click += new System.EventHandler(this.btnRemove$Attribute.Name$_Click);
            // 
            // btnSave$Attribute.Name$
            // 
            this.btnSave$Attribute.Name$.AutoSize = true;
            this.btnSave$Attribute.Name$.Location = new System.Drawing.Point(335, 3);
            this.btnSave$Attribute.Name$.Name = "bSave$Attribute.Name$";
            this.btnSave$Attribute.Name$.Padding = new System.Windows.Forms.Padding(5);
            this.btnSave$Attribute.Name$.Size = new System.Drawing.Size(75, 33);
            this.btnSave$Attribute.Name$.TabIndex = 1;
            this.btnSave$Attribute.Name$.Text = "Save";
            this.btnSave$Attribute.Name$.UseVisualStyleBackColor = true;
            this.btnSave$Attribute.Name$.Click += new System.EventHandler(this.btnSave$Attribute.Name$_Click);
            // 
            // panel$Attribute.Type$
            // 
            this.panel$Attribute.Type$.AutoSize = true;
            this.panel$Attribute.Type$.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel$Attribute.Type$.Location = new System.Drawing.Point(5, 18);
            this.panel$Attribute.Type$.Name = "panel$Attribute.Type$";
            this.panel$Attribute.Type$.Size = new System.Drawing.Size(409, 135);
            this.panel$Attribute.Type$.TabIndex = 0;
            // 
            // gp$Attribute.Type$
            // 
            this.gp$Attribute.Type$.AutoSize = true;
            this.gp$Attribute.Type$.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gp$Attribute.Type$.Controls.Add(this.panel$Attribute.Type$);
            this.gp$Attribute.Type$.Location = new System.Drawing.Point(10, 10);
            this.gp$Attribute.Type$.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.gp$Attribute.Type$.Name = "gp$Attribute.Type$";
            this.gp$Attribute.Type$.Padding = new System.Windows.Forms.Padding(5);
            this.gp$Attribute.Type$.Size = new System.Drawing.Size(419, 158);
            this.gp$Attribute.Type$.TabIndex = 1;
            this.gp$Attribute.Type$.TabStop = false;
            this.gp$Attribute.Type$.Text = "$Attribute.Type$"; 
            // 
            // flpButtons add buttons
            //             
            this.flpButtons.Controls.Add(this.btnSave$Attribute.Name$);
            this.flpButtons.Controls.Add(this.btnRemove$Attribute.Name$);
            this.flpButtons.Controls.Add(this.btnSelect$Attribute.Name$);                       
            this.AcceptButton = this.btnSelect$Attribute.Name$;            
