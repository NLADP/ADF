using System;
using System.Windows.Forms;
using Adf.Business.Binding;
using $UseCase.Model.Name.Pascal$.Business;
using $UseCase.Model.Name.Pascal$.Process;

namespace $UseCase.Model.Name.Pascal$.Win.Forms
{
	public partial class $UseCase.Name.Pascal$
	{
    
		#region CodeGuard(Constructor)
		    
		public $UseCase.Name.Pascal$()
		{
			InitializeComponent();
<Tobago.Include(true,"Constructor")>            
		}
        
		#endregion CodeGuard(Constructor)   
		     
		     
		#region CodeGuard(MyTask)
				     
		private $UseCase.Name.Pascal$Task MyTask
		{
			get { return Task as $UseCase.Name.Pascal$Task; }
		}        

		#endregion CodeGuard(MyTask) 
 		
 		       
		#region CodeGuard(Bindings)
		       
		public override void Bind()
		{
<Tobago.Loop(UseCase.Attributes, "Bindings")>
		}

		#endregion CodeGuard(Bindings)
 		        
		#region CodeGuard(Events)
		        
<Tobago.Loop(UseCase.Attributes, "Events")>        

		#endregion CodeGuard(Events)
        
		private void $UseCase.Name.Pascal$_Load(object sender, EventArgs e)
		{
			Bind();
		}

    }
}