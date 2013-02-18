		#region CodeGuard(Property $Attribute.Name.Pascal$)
		
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")>
		public SmartReference<$Attribute.Name.Pascal$> $Attribute.Name.Pascal$
		{
			get 
			{ 
				return SmartReferenceFactory.Get<$Attribute.Name.Pascal$>(state.GetValue<ID>($Attribute.Owner$Describer.$Attribute.Name.Pascal$)); 
			}
			set 
			{ 
				state.SetValue($Attribute.Owner$Describer.$Attribute.Name.Pascal$, value.Id); 
			}
		}

		#endregion CodeGuard(Property $Attribute.Name.Pascal$)

