
--ALTER TABLE [$Association.MyEnd.Role.Pascal$] WITH CHECK ADD CONSTRAINT [FK_$Association.MyEnd.Role.Pascal$_$Association.OtherEnd.Role.Pascal$] FOREIGN KEY([$Association.OtherEnd.Role.Pascal$ID])
--REFERENCES [$Association.OtherEnd.Target.Name.Pascal$] ([Id])
--GO
--ALTER TABLE [$Association.MyEnd.Role.Pascal$] CHECK CONSTRAINT [FK_$Association.MyEnd.Role.Pascal$_$Association.OtherEnd.Role.Pascal$]
--GO


