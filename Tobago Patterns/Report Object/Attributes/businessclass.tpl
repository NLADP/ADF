		#region CodeGuard(Property $Attribute.Type$)

		private $Attribute.Type.Pascal$ $Attribute.Name.Camel$ = $Attribute.Type.Pascal$.Empty;
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")>
		public $Attribute.Type.Pascal$ $Attribute.Name.Pascal$
		{
			get 
			{
				if ($Attribute.Name.Camel$.IsEmpty) $Attribute.Name.Camel$ = $Attribute.Type.Pascal$Factory.Get(state.GetValue<ID>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$)); 
				
				return $Attribute.Name.Camel$; 
			}
			set 
			{ 
				$Attribute.Name.Camel$ = value;
				
				state.SetValue<ID>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$, value.Id);
			}
		}

		#endregion CodeGuard(Property $Attribute.Type$)

