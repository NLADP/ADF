//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Model           : <Tobago.ProjectName>
//     Template        : Generic Code Behind.tpl
//     Runtime Version : $Version$
//     Generation date : <Tobago.CurrentDate>
//
//     Changes to this file may cause incorrect behavior and may be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Web.UI.WebControls;
using Adf.Base.Authorization;
using Adf.Base.Domain;
using Adf.Core.Identity;
using Adf.Business;
using Adf.Core.Binding;
using Adf.Web.Process;
using Adf.Web.UI.SmartView;
using $UseCase.Model.Name.Pascal$.Business;
using $UseCase.Model.Name.Pascal$.Process;

/// <summary>
/// Summary description for $UseCase.Name.Pascal$.
/// </summary>
public partial class Forms_$UseCase.Name.Pascal$ : $UseCase.Model$View<$UseCase.Name.Pascal$Task>
{
	
	#region Web Form Designer generated code
	override protected void OnInit(EventArgs e)
	{
		//
		// CODEGEN: This call is required by the ASP.NET Web Form Designer.
		//
		InitializeComponent();
<Tobago.Loop(UseCase.Attributes, "Authorization")>

		base.OnInit(e);
	}
	
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{    
	}
	#endregion

	#region CodeGuard(Attributes)
	
<Tobago.Loop(UseCase.Attributes, "Attributes")>
	#endregion CodeGuard(Attributes)

	#region CodeGuard(Bind)
	
	protected override void Bind()
    {
<Tobago.Loop(UseCase.Attributes, "Bind")>
    }
    
	#endregion CodeGuard(Bind)

	#region CodeGuard(Events)
	
<Tobago.Loop(UseCase.Attributes, "Events")>
	#endregion CodeGuard(Events)

	#region CodeGuard(Custom)

	#endregion CodeGuard(Custom)
}

