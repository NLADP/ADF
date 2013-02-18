		#region CodeGuard(Association $Association.OtherEnd.Role$)
		
		// Association $Association.MyEnd.Role$[simple] : $Association.OtherEnd.Role$[many]
		
	    private readonly DomainCollection<$Association.OtherEnd.Target.Name.Pascal$> $Association.OtherEnd.Role.Camel.Plural$ = new DomainCollection<$Association.OtherEnd.Target.Name.Pascal$>();
		public DomainCollection<$Association.OtherEnd.Target.Name.Pascal$> $Association.OtherEnd.Role.Pascal.Plural$
		{
			get
			{
                if (!$Association.OtherEnd.Role.Camel.Plural$.IsInitialised) 
                    $Association.OtherEnd.Role.Camel.Plural$.Add($Association.OtherEnd.Target.Name.Pascal$Factory.GetBy$Association.MyEnd.Role.Pascal$(this));
			    
                return $Association.OtherEnd.Role.Camel.Plural$;
			}
		}
		
		public $Association.OtherEnd.Target.Name.Pascal$ New$Association.OtherEnd.Role.Pascal$()
		{
			$Association.OtherEnd.Target.Name.Pascal$ $Association.OtherEnd.Role.Camel$ = $Association.OtherEnd.Target.Name.Pascal$Factory.New();
			 
			$Association.OtherEnd.Role.Camel$.$Association.MyEnd.Role.Pascal$ = this;

			return $Association.OtherEnd.Role.Camel$;
		}
		
		#endregion CodeGuard(Association $Association.OtherEnd.Role$)

