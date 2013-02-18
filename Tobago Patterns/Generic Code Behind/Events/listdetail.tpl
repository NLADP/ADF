    protected void grd$Attribute.Name.Pascal.Plural$_SelectedIndexChanged(object sender, EventArgs e)
	{
		$Attribute.Name.Pascal$ = $Attribute.Name.Pascal.Plural$[grd$Attribute.Name.Pascal.Plural$.Current];
		Bind();
	}

	protected void lbNew$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
	{
	    MyTask.New$Attribute.Name.Pascal$();

	    Bind();
	}

	protected void lbCancel$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
	{
        MyTask.Cancel();
	}

    protected void lbSave$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
    {
        BindManager.Persist($Attribute.Name.Pascal$, panel$Attribute.Name.Pascal$);

        if (MyTask.Save$Attribute.Name.Pascal$()) Bind();
    }

     protected void lbRemove$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
     {
        MyTask.Remove$Attribute.Name.Pascal$();

		grd$Attribute.Name.Pascal.Plural$.SelectedIndex = -1;

        Bind();
     }
