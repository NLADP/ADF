		#region CodeGuard(Property $Attribute.Name$)
		<Tobago.IfEmpty($Attribute.Doc$,,// $Attribute.Doc$)>
		<Tobago.If($Attribute.IsNullable$,"", "[NonEmpty]")>
		public $Attribute.Type.Pascal$ $Attribute.Name.Pascal$
		{
			get { return $Attribute.Type.Pascal$.Get<$Attribute.Type.Pascal$>(state.Get($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$)); }
			set { state.Set($Attribute.Owner.Name.Pascal$Describer.$Attribute.Name.Pascal$, value.ToString()); }
		}

		#endregion CodeGuard(Property $Attribute.Name$)

