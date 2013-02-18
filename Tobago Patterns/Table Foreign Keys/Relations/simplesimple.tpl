
ALTER TABLE [$Association.MyEnd.Target.Name.Pascal$] WITH CHECK ADD CONSTRAINT [FK_$Association.MyEnd.Role.Pascal$_$Association.OtherEnd.Role.Pascal$] FOREIGN KEY([$Association.OtherEnd.Target.Name.Pascal$ID])
REFERENCES [$Association.OtherEnd.Target.Name.Pascal$] ([Id])
