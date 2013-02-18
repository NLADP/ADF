
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
			// Act
            Task.Continue(ApplicationTask.Main, TaskResult.Ok, new object());

            // Assert
            Task
				.ValidationSucceeded()
                .ViewIsActivated();
        }


		[TestMethod]
		public void SelectValid$Attribute.Name.Pascal$()
		{
            var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();

			// Act
            Task.Select$Attribute.Name.Pascal$($Attribute.Name.Camel$);
            
			// Assert
            Task
				.ValidationSucceeded()
                .IsOk()
                .ReturnsValid(typeof($Attribute.Type.Pascal$), 0);
		}

		[TestMethod]
		public void SelectValidNew$Attribute.Name.Pascal$()
		{
			// Act
			Task.New$Attribute.Name.Pascal$();

            // Assert
            Task
				.ValidationSucceeded()
                .IsOk()
                .ReturnsValid(typeof($Attribute.Type.Pascal$), 0);
		}