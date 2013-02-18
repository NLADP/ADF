        public void Save$Attribute.Name.Pascal$()
        {

			this.ExecuteAndRun(select,
				() => this.Validate(), 
				() => $Attribute.Name.Pascal$.Validate(), 
				() => $Attribute.Name.Pascal$.Save());
        }	
        
		public void Select$Attribute.Name.Pascal$()
		{
			this.RunTask(select);
		}
        
		public void Remove$Attribute.Name.Pascal$()
		{
            this.ExecuteAndRun(select, 
				() => $Attribute.Name.Pascal$.Remove());
		}

