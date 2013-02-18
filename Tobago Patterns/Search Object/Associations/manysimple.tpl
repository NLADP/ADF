
		#region CodeGuard(Attribute $Association.OtherEnd.Role$)

		// Association $Association.MyEnd.Role$[many] : $Association.OtherEnd.Role$[simple]

		private $Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Role.Camel$ = $Association.OtherEnd.Target.Name.Pascal$Factory.Empty;
		
		[Is("$Association.OtherEnd.Role.Pascal$")]
		public $Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Role.Pascal$
		{
			get { return $Association.OtherEnd.Role.Camel$; }
			set { $Association.OtherEnd.Role.Camel$ = value; }
		}

		#endregion CodeGuard(Attribute $Association.OtherEnd.Role$)
		