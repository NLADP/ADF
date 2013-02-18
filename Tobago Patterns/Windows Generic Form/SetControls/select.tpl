            // 
            // gp$Attribute.Type$
            // 
            this.gp$Attribute.Type$.Controls.Add(this.grd$Attribute.Type$);
            this.gp$Attribute.Type$.Location = new System.Drawing.Point(10, 10);
            this.gp$Attribute.Type$.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.gp$Attribute.Type$.Name = "gp$Attribute.Type$";
            this.gp$Attribute.Type$.Padding = new System.Windows.Forms.Padding(5);
            this.gp$Attribute.Type$.Size = new System.Drawing.Size(733, 205);
            this.gp$Attribute.Type$.TabIndex = 0;
            this.gp$Attribute.Type$.TabStop = false;
            this.gp$Attribute.Type$.Text = "$Attribute.Name$";
            // 
            // grd$Attribute.Type$
            // 
            this.grd$Attribute.Type$.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd$Attribute.Type$.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grd$Attribute.Type$.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.grd$Attribute.Type$.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd$Attribute.Type$.Columns.Add(this.$Attribute.Type$Title);
<Tobago.Loop(Attribute.Classifier.Attributes, "AddGridColumns")>            
            this.grd$Attribute.Type$.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd$Attribute.Type$.Location = new System.Drawing.Point(5, 18);
            this.grd$Attribute.Type$.Margin = new System.Windows.Forms.Padding(0);
            this.grd$Attribute.Type$.MultiSelect = false;
            this.grd$Attribute.Type$.Name = "grd$Attribute.Name$";
            this.grd$Attribute.Type$.RowHeadersVisible = false;
            this.grd$Attribute.Type$.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grd$Attribute.Type$.Size = new System.Drawing.Size(723, 182);
            this.grd$Attribute.Type$.TabIndex = 4;
            this.grd$Attribute.Type$.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd$Attribute.Type$_CellDoubleClick); 
            // 
            // $Attribute.Type$Title
            // 
            this.$Attribute.Type$Title.DataPropertyName = "Title";
            this.$Attribute.Type$Title.HeaderText = "$Attribute.Name$";
            this.$Attribute.Type$Title.Name = "$Attribute.Type$Title";
            this.$Attribute.Type$Title.ReadOnly = true;
            this.$Attribute.Type$Title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.$Attribute.Type$Title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
<Tobago.Loop(Attribute.Classifier.Attributes, "SetGridColumns")>