using System;
using System.Collections.Generic;

namespace API.Common.Filters
{
    public class FilterBuilder
    {
        private readonly List<Type> _filters = new List<Type>();

        public FilterBuilder AddScopedFilter<TFilter>() where TFilter : IScopedFilter
        {
            _filters.Add(typeof(TFilter));

            return this;
        }

        public List<Type> Build() => _filters;
    }
}