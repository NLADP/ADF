		#region CodeGuard(Property $Attribute.Name$)
		<Tobago.IfEmpty($Attribute.Doc$,,// $Attribute.Doc$)>
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")>
		public SmartReference<$Attribute.Name.Pascal$> $Attribute.Name.Pascal$
		{
			get 
			{ 
				return SmartReferenceFactory.Get<$Attribute.Name.Pascal$>(state.Get<ID>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$)); 
			}
			set 
			{ 
				state.Set($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$, value.Id); 
			}
		}

		#endregion CodeGuard(Property $Attribute.Name$)

