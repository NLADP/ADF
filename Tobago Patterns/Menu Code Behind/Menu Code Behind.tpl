using System;
using System.Web.UI;
using Adf.Process;
using $Actor.Model$.Process;

public partial class Controls_$Actor.Name.Pascal$Menu : UserControl
{
    protected void lbHome_Click(object sender, EventArgs e)
    {
        TaskManager.Run(ApplicationTasks.Main);
    }

<Tobago.Loop(Actor.NavigableAssociations, Associations)>
}
