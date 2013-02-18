		public void Select$Attribute.Name.Pascal$($Attribute.Type.Pascal$ $Attribute.Type.Camel$)
		{
			OK($Attribute.Type.Camel$);
		}	
		
		public void New$Attribute.Name.Pascal$()
		{
			OK($Attribute.Type.Pascal$Factory.New());
		}
	
		public void Search$Attribute.Name.Pascal$()
        {
			if (this.Execute(() => $Attribute.Name.Pascal$Search.Validate()))
            {
               $Attribute.Name.Plural.Pascal$ = $Attribute.Type.Pascal$Factory.Search($Attribute.Name.Pascal$Search);
                
                this.ActivateView();
            }
        }