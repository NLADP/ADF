
        #region CodeGuard(Property $Attribute.Name$ Test)

        /// <summary>
        /// A test for property $Attribute.Name$
        /// </summary>
        [TestMethod]
        public void $Attribute.Name$Test()
        {
            $Attribute.Type$ $Attribute.Name.Camel$ = Isbn.New("111-1111-11-1");
            $Attribute.Owner.Name$ $Attribute.Owner.Name.Camel$ = $Attribute.Owner.Name$Factory.New();
            $Attribute.Owner.Name.Camel$.$Attribute.Name$ = $Attribute.Name.Camel$;
            Assert.AreEqual($Attribute.Owner.Name.Camel$.$Attribute.Name$, $Attribute.Name.Camel$, "$Attribute.Owner.Name$. Property $Attribute.Name$ not correctly set.");
        }

        #endregion CodeGuard(Property $Attribute.Name$ Test)
