
		#region CodeGuard(Association $Association.OtherEnd.Role$)

		// Association $Association.MyEnd.Role$[many] : $Association.OtherEnd.Role$[simple]

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static DomainCollection<$Association.MyEnd.Target.Name.Pascal$> GetBy$Association.OtherEnd.Role.Pascal$($Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Target.Name.Camel$)
		{
			if ($Association.OtherEnd.Target.Name.Camel$ == null) throw new ArgumentNullException("$Association.OtherEnd.Target.Name.Camel$");
			
            return new DomainCollection<$Association.MyEnd.Target.Name.Pascal$>(from state in $Association.MyEnd.Target.Name.Pascal$Gateway.GetBy$Association.OtherEnd.Role.Pascal$($Association.OtherEnd.Target.Name.Camel$.Id) select new $Association.MyEnd.Target.Name.Pascal$(state) { $Association.OtherEnd.Role.Pascal$ = $Association.OtherEnd.Target.Name.Camel$} );
		}

		#endregion CodeGuard(Association $Association.OtherEnd.Role$)