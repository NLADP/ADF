		#region CodeGuard(Property $Attribute.Name$)
		<Tobago.IfEmpty($Attribute.Doc$,,// $Attribute.Doc$)>
		private $Attribute.Type.Pascal$ $Attribute.Name.Camel$;
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")>
		public $Attribute.Type.Pascal$ $Attribute.Name.Pascal$
		{
			get 
			{
				return $Attribute.Name.Camel$ ?? ($Attribute.Name.Camel$ = $Attribute.Type.Pascal$Factory.Get(state.Get<ID>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$))); 				 
			}
			set 
			{ 
				$Attribute.Name.Camel$ = value;
				
				state.Set<ID>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$, value.Id);
			}
		}

		#endregion CodeGuard(Property $Attribute.Name$)

