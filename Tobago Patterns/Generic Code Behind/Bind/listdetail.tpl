		BindManager.Bind(grd$Attribute.Name.Pascal.Plural$, $Attribute.Name.Pascal.Plural$);
		BindManager.Bind(panel$Attribute.Name.Pascal$, $Attribute.Name.Pascal$);

		if(!$Attribute.Name.Pascal$.IsEmpty)
		{
			grd$Attribute.Name.Pascal.Plural$.Current = $Attribute.Name.Pascal$.Id;
		}