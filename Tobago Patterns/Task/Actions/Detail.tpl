		public void New$Attribute.Name.Pascal$()
		{
			//TODO: replace Owner clause by actual applicable clause
			
            this.RunTask($Attribute.Model.Name.Pascal$Task.Define$Attribute.Type.Pascal$, Owner.New$Attribute.Name.Pascal$());
        }

		public void Select$Attribute.Name.Pascal$($Attribute.Type.Pascal$ $Attribute.Name.Camel$)
		{
            this.RunTask($Attribute.Model.Name.Pascal$Task.Define$Attribute.Type.Pascal$, $Attribute.Name.Camel$);
        }

        public void Remove$Attribute.Name.Pascal$($Attribute.Type.Pascal$ $Attribute.Name.Camel$)
        {
            if (this.Execute(
				() => $Attribute.Name.Plural.Pascal$.RemoveItem($Attribute.Name.Camel$),
				() => $Attribute.Name.Camel$.Remove()
				))
            {
	            this.ActivateView();
            }
        }
        