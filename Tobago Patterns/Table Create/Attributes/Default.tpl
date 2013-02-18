
	<Tobago.If($Attribute.Derived$,--,)>[$Attribute.Name.Pascal$] varchar(<Tobago.Tag(maxlength, 255)>) <Tobago.If($Attribute.IsNullable$,NULL, NOT NULL)>,