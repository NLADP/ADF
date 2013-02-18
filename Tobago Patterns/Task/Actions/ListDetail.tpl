		public bool Save$Attribute.Name.Pascal$()
        {
			if (this.Execute(
				() => this.Validate(),
				() => $Attribute.Type.Pascal$.Validate(), 
				() => $Attribute.Type.Pascal$.Save())
			)
            {
                $Attribute.Name.Pascal.Plural$.AddItem($Attribute.Name.Pascal$);

				return true;
            }

			return false;
        }

        public void Remove$Attribute.Name.Pascal$()
        {
            if (this.Execute(() => $Attribute.Name.Pascal$.Remove()))
            {
                $Attribute.Name.Pascal.Plural$.Remove($Attribute.Name.Pascal$);

				New$Attribute.Name.Pascal$();
            }
        }

	    public void New$Attribute.Name.Pascal$()
	    {
	        $Attribute.Name.Pascal$ = $Attribute.Type.Pascal$Factory.New();

            $Attribute.Name.Pascal$.Name = "[New]";
	    }