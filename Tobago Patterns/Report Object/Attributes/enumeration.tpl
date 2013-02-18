		#region CodeGuard(Property $Attribute.Name.Pascal$)
		
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")>
		public $Attribute.Type.Pascal$ $Attribute.Name.Pascal$
		{
			get
			{
				$Attribute.Type.Pascal$ $Attribute.Type.Camel$;

                if (!Enum.TryParse(state.Get<string>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$), out $Attribute.Type.Camel$))
                {
                    $Attribute.Type.Camel$ = $Attribute.Type.Pascal$.Empty;
                }
			    return $Attribute.Type.Camel$;
			}
			set
			{
				state.Set<string>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$, value.ToString());
			}
		}

		#endregion CodeGuard(Property $Attribute.Name.Pascal$)

