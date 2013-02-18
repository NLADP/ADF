		
    protected void lbSelect$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
    {
        MyTask.Select$Attribute.Name.Pascal$();
    }

    protected void lbDelete$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
    {
        MyTask.Remove$Attribute.Name.Pascal$();
    }

    protected void lbSave$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
    {
        BindManager.Persist($Attribute.Name.Pascal$, this);
        
        MyTask.Save$Attribute.Name.Pascal$();
    }

    protected void lbCancel$Attribute.Name.Pascal$_Click(object sender, EventArgs e)
    {
        MyTask.Cancel();
    }		
