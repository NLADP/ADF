using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Adf.Core.Objects;

namespace Adf.Core.Domain
{
    public class DomainMapper
    {
        private static IObjectMappingProvider _mapper = null;
        protected static IObjectMappingProvider mapper
        {
            get
            {
                _mapper = _mapper ?? ObjectFactory.BuildUp<IObjectMappingProvider>();

                return _mapper;
            }
        }

        public static TDomainObject Bind<TDomainObject, TSource>(TDomainObject target, TSource source, Mapping<TDomainObject, TSource> mapping) where TDomainObject : IDomainObject
        {
            return mapper.Bind(target, source, mapping);
        }

        public static TTarget Persist<TDomainObject, TTarget>(TDomainObject source, TTarget target, Mapping<TDomainObject, TTarget> mapping) where TDomainObject : IDomainObject
        {
            return mapper.Persist(source, target, mapping);
        }

    }

    public class Mapping<TDomainObject, TDto> : Dictionary<Expression<Func<TDomainObject, object>>, Expression<Func<TDto, object>>>
    {
    }

}
