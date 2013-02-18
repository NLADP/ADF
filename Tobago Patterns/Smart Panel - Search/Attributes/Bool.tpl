			Add(ValidatedCheckBoxItem.Create<$Attribute.Owner.Name.Pascal$SearchObject>("$Attribute.Name$", o => o.$Attribute.Name.Pascal$, Editable<Tobago.If($Attribute.IsNullable$, ", Optional", "")>));
