using System;
using System.Collections.Generic;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Data.Search;

namespace Adf.Business.Search
{
    /// <summary>
    /// Represents SearchObject that contains search parameters.
    /// </summary>
    [Serializable]
    public class SearchObject : ISearchObject
    {
        /// <summary>
        /// Returns a list of SearchParameters.
        /// </summary>
        /// <returns>A list of SearchParameters.</returns>
        public virtual IEnumerable<ISearchParameter> GetParameters()
        {
            var parameters = new List<ISearchParameter>();

            PropertyInfo[] properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                foreach (SearchAttribute search in property.GetCustomAttributes(typeof(SearchAttribute), false))
                {
                    search.Value = property.GetValue(this, null);

                    if (search.Value != null && (search.IncludeWhenEmpty || !PropertyHelper.IsEmpty(search.Value)))
                    {
                        parameters.Add(search);
                    }
                }
            }

            return parameters;
        }

        public virtual IEnumerable<IJoinParameter> GetJoinParameters()
        {
            throw new NotImplementedException();
        }
    }
}