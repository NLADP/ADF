        [TestMethod]
        public void StartWithValid$Attribute.Name.Pascal$()
        {
            var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();

			// Act
            Task.Init($Attribute.Name.Camel$);

            // Assert
            Task
				.ValidationSucceeded()
                .ViewIsActivated()
                .IsEqual(t => t.$Attribute.Name.Pascal$, $Attribute.Name.Camel$);
        }


        [TestMethod]
        public void StartWithout$Attribute.Name.Pascal$()
        {
			// Act
            Task.Start();

            // Assert
            Task.OtherTaskIsStarted($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$);
        }

        [TestMethod]
		[ExpectedException(typeof(InvalidCastException))]
        public void StartWithInvalid$Attribute.Name.Pascal$()
        {
            Task.Init(null);
        }

        [TestMethod]
        public void ContinueWithInvalidResult()
        {
			// Act
            Task.Continue(ApplicationTask.Main, TaskResult.Error);

            // Assert
            Task
				.ValidationSucceeded()
				.ViewIsActivated();
        }

        [TestMethod]
        public void ContinueWithCancel()
        {
			// Act
            task.Continue($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$, TaskResult.Cancel);

            // Assert
            Task
				.ValidationSucceeded()
                .ViewIsDeactivated()
                .IsCancelled();
        }

        [TestMethod]
		[ExpectedException(typeof(InvalidCastException))]
        public void ContinueWithOkButInvalid$Attribute.Name.Pascal$()
        {
            Task.Continue($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$, TaskResult.Ok, new object());
        }

        [TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
        public void ContinueWithOkButWithout$Attribute.Name.Pascal$()
        {
            Task.Continue($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$, TaskResult.Ok);
        }

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
        public void ContinueWithOkButWithNull()
        {
            Task.Continue($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$, TaskResult.Ok, null);
        }

        [TestMethod]
        public void ContinueWithOkAndValid$Attribute.Name.Pascal$()
        {
            var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();

			// Act
            Task.Continue($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$, TaskResult.Ok, $Attribute.Name.Camel$);

            // Assert
            Task
				.ValidationSucceeded()
				.IsEqual(t => t.$Attribute.Name.Pascal$, $Attribute.Name.Camel$)
                .ViewIsActivated();
        }

        [TestMethod]
        public void SaveInvalid$Attribute.Name.Pascal$()
        {
            var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();
			
			// TODO: invalidate one or more properties of $Attribute.Name.Camel$, so validation fails	
            $Attribute.Name.Camel$.Name = null;

			// Act
            Task.Init($Attribute.Name.Camel$);

            // Assert
            Task
				.ValidationSucceeded()
				.ViewIsActivated();

            Task.Save$Attribute.Name.Pascal$();

            //Assertions
            Task
                .ValidationFailed()
				.ViewIsActivated();
        }

        [TestMethod]
        public void SaveValid$Attribute.Name.Pascal$()
        {
			// TODO: check the values of the attributes of the $Attribute.Name.Camel$
			var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();
            
			// Act
			Task.Init($Attribute.Name.Camel$);

			// Assert
			Task
				.ValidationSucceeded()
				.ViewIsActivated();

			// Act
			Task.Save$Attribute.Name.Pascal$();

			// Assert
			Task.ValidationSucceeded();
			Assert.AreNotEqual($Attribute.Type.Pascal$Factory.Get($Attribute.Name.Camel$.Id), $Attribute.Type.Pascal$Factory.Empty);

			// Act
			Task.Remove$Attribute.Name.Pascal$();

			// Assert
			Task.ValidationSucceeded();
			Assert.AreEqual($Attribute.Type.Pascal$Factory.Get($Attribute.Name.Camel$.Id), $Attribute.Type.Pascal$Factory.Empty);
        }

        [TestMethod]
        public void Select$Attribute.Name.Pascal$()
        {
			// Act
            Task.Select$Attribute.Name.Pascal$();

            // Assert
            Task
				.ValidationSucceeded()
				.OtherTaskIsStarted($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$);
        }
