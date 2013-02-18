
		#region CodeGuard(Association $Association.OtherEnd.Role$)

		// Association $Association.MyEnd.Role$[many] : $Association.OtherEnd.Role$[simple]

		public static DomainCollection<$Association.MyEnd.Target.Name.Pascal$> GetBy$Association.OtherEnd.Role.Pascal$($Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Role.Camel$)
		{
			if ($Association.OtherEnd.Role.Camel$ == null) throw new ArgumentNullException("$Association.OtherEnd.Role.Camel$");
			if ($Association.OtherEnd.Role.Camel$.IsEmpty) return new DomainCollection<$Association.MyEnd.Target.Name.Pascal$>();
			
			return new DomainCollection<$Association.MyEnd.Target.Name.Pascal$>(GetAll().Where($Association.MyEnd.Target.Name.Camel$ => $Association.MyEnd.Target.Name.Camel$.$Association.OtherEnd.Role.Pascal$ == $Association.OtherEnd.Role.Camel$));
		}

		#endregion CodeGuard(Association $Association.OtherEnd.Role$)