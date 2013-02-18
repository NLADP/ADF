	protected void grd$Attribute.Name.Plural.Pascal$_SelectedIndexChanged(object sender, EventArgs e)
	{
		DomainCollection<$Attribute.Type.Pascal$> selected = new DomainCollection<$Attribute.Type.Pascal$>();
	
		foreach (GridViewRow gridRow in grd$Attribute.Name.Pascal$.Rows)
		{
			CheckBox check = (CheckBox)gridRow.FindControl("chkSelect");
			
			if (check.Checked) selected.Add($Attribute.Name.Plural.Pascal$[gridRow.DataItemIndex]);
		}

		MyTask.Select$Attribute.Name.Pascal$(selected);
	}

	protected void lbNew$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
	{
        MyTask.New$Attribute.Name.Pascal$();
	}

	protected void lbCancel$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
	{
        MyTask.Cancel();
	}
	
