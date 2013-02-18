
		#region CodeGuard(Association $Association.OtherEnd.Role$)
		
		public static IEnumerable<IInternalState> GetBy$Association.OtherEnd.Role.Pascal$(ID $Association.OtherEnd.Role.Camel$Id)
		{
			if ($Association.OtherEnd.Role.Camel$Id.IsEmpty) return Enumerable.Empty<IInternalState>();

			return new AdfQuery()
				.Select()
				.From($Association.MyEnd.Target.Name.Pascal$Describer.Table)
				.Where($Association.MyEnd.Target.Name.Pascal$Describer.$Association.OtherEnd.Role.Pascal$).IsEqual($Association.OtherEnd.Role.Camel$Id)
				.RunSplit(DataSource);
		}

		#endregion CodeGuard(Association $Association.OtherEnd.Role$)
