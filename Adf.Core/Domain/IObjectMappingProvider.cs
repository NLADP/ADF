using System;

namespace Adf.Core.Domain
{
    public interface IObjectMappingProvider
    {
        /// <summary>
        /// Binds the field for a domainobject to item from the source object.
        /// </summary>
        /// <param name="target">Object to bind fields in.</param>
        /// <param name="source">Source used for binding fields, such as DataSet, RowState or IInternalState.</param>
        /// <param name="mapping"></param>
        TDomainObject Bind<TDomainObject, TSource>(TDomainObject target, TSource source, Mapping<TDomainObject, TSource> mapping);

        /// <summary>
        /// Binds the field for a domainobject to item from the source object.
        /// </summary>
        /// <param name="source">Object to bind fields from to target object.</param>
        /// <param name="target">Target used to binding fields to, and persist to a data source later on.</param>
        /// <param name="mapping"></param>
        TTarget Persist<TDomainObject, TTarget>(TDomainObject source, TTarget target, Mapping<TDomainObject, TTarget> mapping);
    }
}
