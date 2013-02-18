		
		// detail $Attribute.Name.Pascal$

		[TestMethod]
        public void ContinueDetailWithCancel()
        {
			// TODO: replace Define clause by actual applicable clause
            Task.Continue($Attribute.Classifier.Model$Task.Define$Attribute.Type.Pascal$, TaskResult.Cancel);

            // Assert
            Task
				.ValidationSucceeded()
				.ViewIsActivated();
        }

		[TestMethod]
		[ExpectedException(typeof(InvalidCastException))]
        public void ContinueDetailWithOkButInvalid$Attribute.Name.Pascal$()
        {
			// TODO: replace Define clause by actual applicable clause
            Task.Continue($Attribute.Classifier.Model$Task.Define$Attribute.Type.Pascal$, TaskResult.Ok, new object());
        }

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
        public void ContinueDetailWithOkButWithout$Attribute.Name.Pascal$()
        {
			// TODO: replace Define clause by actual applicable clause
            Task.Continue($Attribute.Classifier.Model$Task.Define$Attribute.Type.Pascal$, TaskResult.Ok);
        }

		[TestMethod]
		[ExpectedException(typeof(NullReferenceException))]
        public void ContinueDetailWithOkButWithNull()
        {
			// TODO: replace Define clause by actual applicable clause
            Task.Continue($Attribute.Classifier.Model$Task.Define$Attribute.Type.Pascal$, null);
        }

		[TestMethod]
        public void ContinueDetailWithOkAndValid$Attribute.Name.Pascal$()
        {
			var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();

			// TODO: replace Define clause by actual applicable clause
            Task.Continue($Attribute.Classifier.Model$Task.Define$Attribute.Type.Pascal$, TaskResult.Ok, $Attribute.Name.Camel$);

            // Assert
            Task
				.ValidationSucceeded()
				.ViewIsActivated();
        }

		[TestMethod]
		public void New$Attribute.Name.Pascal$()
		{
			Task.New$Attribute.Name.Pascal$();

			// TODO: replace Define clause by actual applicable clause
			// Assert
			Task
				.ValidationSucceeded()
				.OtherTaskIsStarted($Attribute.Classifier.Model$Task.Define$Attribute.Type.Pascal$);
		}

		[TestMethod]
		public void SelectValid$Attribute.Name.Pascal$()
		{
            var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();

			// Act
            Task.Select$Attribute.Name.Pascal$($Attribute.Name.Camel$);
            
			// TODO: replace Define clause by actual applicable clause
			// Assert
            Task
				.ValidationSucceeded()
				.OtherTaskIsStarted($Attribute.Classifier.Model$Task.Define$Attribute.Type.Pascal$);
		}


		[TestMethod]
		public void Remove$Attribute.Name.Pascal$()
		{
			var $Attribute.Name.Camel$ = $Attribute.Type.Pascal$MockFactory.Create();

			// Act
			Task.Remove$Attribute.Name.Pascal$($Attribute.Name.Camel$);

			//Assert
			task
				.ValidationSucceeded()
				.ViewIsActivated();

			Assert.AreEqual($Attribute.Type.Pascal$Factory.Get($Attribute.Name.Camel$.Id), $Attribute.Type.Pascal$Factory.Empty);
		}