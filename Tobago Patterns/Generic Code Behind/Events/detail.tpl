    protected void grd$Attribute.Name.Plural.Pascal$_SelectedIndexChanged(object sender, EventArgs e)
	{
        BindManager.Persist(Owner, this);

		MyTask.Select$Attribute.Name.Pascal$($Attribute.Name.Plural.Pascal$[grd$Attribute.Name.Plural.Pascal$.Current]);
	}

    protected void grd$Attribute.Name.Plural.Pascal$_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ID id;

        if (grd$Attribute.Name.Plural.Pascal$.TryGetId(e.RowIndex, out id)) MyTask.Remove$Attribute.Name.Pascal$($Attribute.Name.Plural.Pascal$[id]);
    }

	protected void lbNew$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
	{
        BindManager.Persist(Owner, this);

        MyTask.New$Attribute.Name.Pascal$();
	}
