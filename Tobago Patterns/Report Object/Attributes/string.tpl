		#region CodeGuard(Property $Attribute.Name.Pascal$) 
		
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")> [MaxLength(<Tobago.Tag(MaxLength, 255)>)]
		public $Attribute.Type$ $Attribute.Name.Pascal$
		{
			get { return state.Get<$Attribute.Type$>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$); }
			set { state.Set($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$, value); }
		}
		
		#endregion CodeGuard(Property $Attribute.Name.Pascal$)

