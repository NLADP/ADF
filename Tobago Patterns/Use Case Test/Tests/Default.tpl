
        [TestMethod]
        public void StartWithValid$Attribute.Name.Pascal$()
        {
			// Act
            Task.Start();

			// Assert
            Task
				.ValidationSucceeded()
				.ViewIsActivated();
        }

		[TestMethod]
        public void StartWithInvalid$Attribute.Name.Pascal$()
        {
			// Act
			Task.Start(null);

            // Assert
            Task
				.ValidationSucceeded()
                .ViewIsDeactivated()
                .IsCancelled();
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
            Task.Continue($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$, TaskResult.Cancel);

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
        public void ContinueWithOkAndValid$Attribute.Name.Pascal$()
        {
            Task.Continue($Attribute.Classifier.Model$Task.Select$Attribute.Type.Pascal$, TaskResult.Ok, $Attribute.Name.Camel$);

            // Assert
            Task
				.ValidationSucceeded()
				.ViewIsActivated();
        }
