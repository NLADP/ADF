            this.gp$Attribute.Type$ = new System.Windows.Forms.GroupBox();
            this.grd$Attribute.Type$ = new Adf.Win.BusinessGridView();
            this.$Attribute.Type$Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
<Tobago.Loop(Attribute.Classifier.Attributes, "InitializeGridColumns")>            
            this.gp$Attribute.Type$.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd$Attribute.Type$)).BeginInit();
            this.flpMain.Controls.Add(this.gp$Attribute.Type$);
                    