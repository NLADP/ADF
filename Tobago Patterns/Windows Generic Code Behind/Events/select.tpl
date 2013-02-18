		private void btnOk_Click(object sender, EventArgs e)
		{
		    HandleOK();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			HandleCancel();
		}
		
        private void btnNew_Click(object sender, EventArgs e)
        {
			DialogResult = DialogResult.OK;
            MyTask.New$Attribute.Type$();
        }		

        private void Select$Attribute.Type$_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel) HandleCancel();
        }

	    private void HandleCancel()
	    {
			DialogResult = DialogResult.Cancel;
            MyTask.Cancel();
	    }

		private void grd$Attribute.Type$_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
            DialogResult = DialogResult.OK;
            HandleOK();
		}
			    
		private void HandleOK()
	    {
			$Attribute.Type$ $Attribute.Type.Lower$ = grd$Attribute.Type$.Current as $Attribute.Type$;
	        if ($Attribute.Type.Lower$ == null) return;
	        MyTask.Select$Attribute.Type$($Attribute.Type.Lower$);
	    }