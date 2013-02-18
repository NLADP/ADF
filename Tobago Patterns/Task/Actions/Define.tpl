        public void Save$Attribute.Name.Pascal$()
        {
			if (this.Execute(
				() => this.Validate(), 
				() => $Attribute.Type.Pascal$.Validate()))
            {
                OK($Attribute.Name.Pascal$);
            }
        }