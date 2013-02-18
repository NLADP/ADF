		#region CodeGuard(Property $Attribute.Name.Pascal$)
		
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")>
		public $Attribute.Type.Pascal$ $Attribute.Name.Pascal$
		{
			get
			{
				return ($Attribute.Type.Pascal$) $Attribute.Type.Pascal$.Get(typeof($Attribute.Type$), state.Get($Attribute.Owner$Describer.$Attribute.Name.Pascal$)); 
			}
			set
			{
				state.Set($Attribute.Owner$Describer.$Attribute.Name.Pascal$, value.ToString());
			}
		}

		#endregion CodeGuard(Property $Attribute.Name.Pascal$)

