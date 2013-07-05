using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adf.Core.Validation;
using Adf.Data.Search;

namespace Adf.Business.Search
{
    public static class SearchObjectExtensions
    {
        public static void Validate(this ISearchObject searchObject)
        {
            ValidationManager.Validate(searchObject);
        }
    }
}
