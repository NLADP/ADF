
<Tobago.If($Attribute.Derived$,--,)>ALTER TABLE [$Attribute.Owner.Name.Pascal$] WITH CHECK ADD CONSTRAINT [FK_$Attribute.Name.Pascal$_$Attribute.Type.Pascal$] FOREIGN KEY([$Attribute.Type.Pascal$ID])
REFERENCES [$Attribute.Type.Pascal$] ([Id])
