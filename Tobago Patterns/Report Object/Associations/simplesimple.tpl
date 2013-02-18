		#region CodeGuard(Association $Association.OtherEnd.Role$) 

		// Association $Association.MyEnd.Role$[many] : $Association.OtherEnd.Role$[simple]

		private $Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Role.Camel$ = $Association.OtherEnd.Target.Name.Pascal$.Empty;
		public $Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Role.Pascal$
		{
			get 
			{
				if ($Association.OtherEnd.Role.Camel$.IsEmpty) $Association.OtherEnd.Role.Camel$ = $Association.OtherEnd.Target$Factory.Get(state.GetValue<ID>($Association.MyEnd.Target$Describer.$Association.OtherEnd.Role.Pascal$)); 
				
				return $Association.OtherEnd.Role.Camel$; 
			}
			set 
			{ 
				$Association.OtherEnd.Role.Camel$ = value;
				
				state.SetValue($Association.MyEnd.Target$Describer.$Association.OtherEnd.Role.Pascal$, value.Id);
			}
		}

		#endregion CodeGuard(Association $Association.OtherEnd.Role$)

