
		#region CodeGuard(Attribute $Attribute.Name.Pascal$) 
		
        private SmartReference<$Attribute.Name.Pascal$> $Attribute.Name.Camel$ = SmartReferenceFactory.GetEmpty<$Attribute.Name.Pascal$>();

        [Is("$Attribute.Name.Pascal$")]
        public SmartReference<$Attribute.Name.Pascal$> $Attribute.Name.Pascal$
        {
            get { return $Attribute.Name.Camel$; }
            set { $Attribute.Name.Camel$ = value; }
        }
        
		#endregion CodeGuard(Attribute $Attribute.Name.Pascal$)
