		#region CodeGuard(Property $Attribute.Name$ )
		<Tobago.IfEmpty($Attribute.Doc$,,// $Attribute.Doc$)>
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")>[InSqlSmallDateTimeRange]
		public $Attribute.Type.Pascal$? $Attribute.Name.Pascal$
		{
			get
			{
				return state.Get<$Attribute.Type.Pascal$>($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$); 
			}
			set
			{
				state.Set($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$, value);
			}
		}

		#endregion CodeGuard(Property $Attribute.Name$)

