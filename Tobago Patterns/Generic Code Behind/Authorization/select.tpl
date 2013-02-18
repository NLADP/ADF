	// TODO: Templates for generating authorization might require some rework. Please attend to it, and remove this comment when done.

    lbNew$Attribute.Name.Pascal$.SetPermission<$Attribute.Type.Pascal$>(Actions.Edit);

	if (MyTask.Origin.Name != $Attribute.Model.Name.Pascal$Task.Manage$Attribute.Type.Pascal$)
    {
        lbNew$Attribute.Type.Pascal$.Visible = false;
    }