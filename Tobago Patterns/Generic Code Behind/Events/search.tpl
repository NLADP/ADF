	protected void grd$Attribute.Name.Plural.Pascal$_SelectedIndexChanged(object sender, EventArgs e)
	{
		MyTask.Select$Attribute.Name.Pascal$($Attribute.Name.Plural.Pascal$[grd$Attribute.Name.Plural.Pascal$.Current]);
	}

	protected void lbNew$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
	{
		MyTask.New$Attribute.Name.Pascal$();
	}

	protected void lbCancel$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
	{
		MyTask.Cancel();
	}
	
	protected void lbSearch$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
    {		
        BindManager.Persist($Attribute.Name.Pascal$Search, searchPanel$Attribute.Name.Pascal$);
        
        MyTask.Search$Attribute.Name.Pascal$();
    }

