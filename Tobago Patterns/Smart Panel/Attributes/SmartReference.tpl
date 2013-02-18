			Add(ValidatedDropDownListItem.Create<$Attribute.Owner.Name.Pascal$>("$Attribute.Name$", o => o.$Attribute.Name.Pascal$, 50, Editable<Tobago.If($Attribute.IsNullable$, ", Optional", "")>));
			