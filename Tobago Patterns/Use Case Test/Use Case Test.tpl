using System;
using Adf.Core;
using Adf.Core.Tasks;
using Adf.Core.Test;
using Adf.Core.Validation;
using Adf.Base.Domain;
using Adf.Test;
using Adf.Test.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using $UseCase.Model$.Business;
using $UseCase.Model$.Test.Mocks;
using $UseCase.Model$.Test;

namespace $UseCase.Model$.Process.Test
{
    [TestClass]
    public class $UseCase.Name.Pascal$Test : TitanTaskTest<$UseCase.Name.Pascal$Task>
    {
		<Tobago.Loop(UseCase.Attributes, "Tests")>
    }
}
