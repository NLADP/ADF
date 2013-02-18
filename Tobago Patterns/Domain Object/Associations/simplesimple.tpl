		#region CodeGuard(Association $Association.OtherEnd.Role$) 

		// Association $Association.MyEnd.Role$[simple] : $Association.OtherEnd.Role$[simple]
		
		private $Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Role.Camel$;
		<Tobago.If($Association.OtherEnd.IsNullable$,"", "[NonEmpty]")>
		public $Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Role.Pascal$
		{
			get 
			{
				return $Association.OtherEnd.Role.Camel$ ?? ($Association.OtherEnd.Role.Camel$ = $Association.OtherEnd.Target.Name.Pascal$Factory.Get(state.Get<ID>($Association.MyEnd.Target.Name.Pascal$Describer.$Association.OtherEnd.Role.Pascal$)));
			}
			set 
			{ 
				$Association.OtherEnd.Role.Camel$ = value;
				
				state.Set($Association.MyEnd.Target.Name.Pascal$Describer.$Association.OtherEnd.Role.Pascal$, value.Id);
			}
		}

		#endregion CodeGuard(Association $Association.OtherEnd.Role$)

