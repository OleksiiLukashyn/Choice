using ChoiceA.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ChoiceA.Attributes
{
    public sealed class RedBeautyAttribute : Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
            new RedBeautyFilter();

        public bool IsReusable => false;
    }
}
