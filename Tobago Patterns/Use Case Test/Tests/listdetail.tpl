
        [TestMethod]
        public void Start()
        {
			// Act
            Task.Start();

            // Assert
            Task
				.ValidationSucceeded()
                .ViewIsActivated()
                .IsNotNull(t => t.$Attribute.Name.Pascal.Plural$);
        }

		[TestMethod]
        public void Continue()
        {
            Task.Continue(ApplicationTask.Main, TaskResult.Ok, new object());
        }

		[TestMethod]
        public void SaveInvalid$Attribute.Name.Pascal$()
        {
            Task.Start(); //start calls the function New$Attribute.Name.Pascal$

			var $Attribute.Name.Camel$ = Task.$Attribute.Name.Pascal$;
			
			// TODO: invalidate one or more properties of $Attribute.Name.Camel$, so validation fails	
            $Attribute.Name.Camel$.Name = null;

            Task.ViewIsActivated();

            Task.Save$Attribute.Name.Pascal$();

            // Assert
            Task
                .ValidationFailed()
				.ViewIsActivated();
        }
		
		[TestMethod]
        public void SaveAndRemove$Attribute.Name.Pascal$()
        {
            Task.Start(); //start calls the function New$Attribute.Name.Pascal$

            Task.ViewIsActivated();

			var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();
			Task.$Attribute.Name.Pascal$ = $Attribute.Name.Camel$;
			
            Task.Save$Attribute.Name.Pascal$();

            // Assert
            Task.ValidationSucceeded();
			Assert.AreNotEqual($Attribute.Type.Pascal$Factory.Get($Attribute.Name.Camel$.Id), $Attribute.Type.Pascal$Factory.Empty);

            Task.Remove$Attribute.Name.Pascal$();

            // Assert
            Task.ValidationSucceeded();
			Assert.AreEqual($Attribute.Type.Pascal$Factory.Get($Attribute.Name.Camel$.Id), $Attribute.Type.Pascal$Factory.Empty);
        }

		[TestMethod]
        public void New$Attribute.Name.Pascal$()
        {
            Task.Start();

            var $Attribute.Name.Camel$ = Task.$Attribute.Name.Pascal$;

            Task.New$Attribute.Name.Pascal$();

            Assert.AreNotEqual($Attribute.Name.Camel$, Task.$Attribute.Name.Pascal$);
        }