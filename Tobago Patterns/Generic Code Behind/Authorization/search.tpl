	// TODO: Templates for generating authorization might require some rework. Please attend to it, and remove this comment when done.

    searchPanel$Attribute.Name.Pascal$.SetPermission<$Attribute.Type.Pascal$>(Actions.View);
    grd$Attribute.Name.Pascal.Plural$.SetPermission<$Attribute.Type.Pascal$>(Actions.View);
	lbNew$Attribute.Name.Pascal$.SetPermission<$Attribute.Type.Pascal$>(Actions.Edit);
    lbSearch$Attribute.Name.Pascal$.SetPermission<$Attribute.Type.Pascal$>(Actions.View);

	if (MyTask.Origin.Name != $Attribute.Model.Name.Pascal$Task.Manage$Attribute.Type.Pascal$)
    {
        lbNew$Attribute.Type.Pascal$.Visible = false;
    }