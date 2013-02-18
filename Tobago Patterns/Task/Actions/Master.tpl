		public void Save$Attribute.Name.Pascal$()
        {
			this.ExecuteAndRun(ApplicationTasks.Select$Attribute.Type.Pascal$,
				() => this.Validate(), 
				() => Master.Validate(), 
				() => Master.Save(), 
				() => SaveDetails()));
        }	
        
		public void Select$Attribute.Name.Pascal$()
		{
			this.RunTask(ApplicationTasks.Select$Attribute.Type.Pascal$);
		}
        
		public void Remove()
		{
            this.ExecuteAndRun(select, 
				() => $Attribute.Type.Pascal$.Remove(), 
				()=> RemoveDetails()));
		}
	