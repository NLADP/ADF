
        #region CodeGuard(Property $Attribute.Name$ Test)

        /// <summary>
        /// A test for property $Attribute.Name$
        /// </summary>
        [TestMethod]
        public void $Attribute.Name$Test()
        {
            $Attribute.Type$ $Attribute.Name.Camel$ = 2.00;
            $Attribute.Owner.Name$ $Attribute.Owner.Name.Camel$ = $Attribute.Owner.Name$Factory.New();
            $Attribute.Owner.Name.Camel$.$Attribute.Name$ = $Attribute.Name.Camel$;
            Assert.AreEqual($Attribute.Owner.Name.Camel$.$Attribute.Name$, $Attribute.Name.Camel$, "$Attribute.Owner.Name$. Property $Attribute.Name$ not correctly set.");
        }

        #endregion CodeGuard(Property $Attribute.Name$ Test)
