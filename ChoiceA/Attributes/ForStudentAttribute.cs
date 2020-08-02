using ChoiceA.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ChoiceA.Attributes
{
    public class ForStudentAttribute : Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
            new ForStudentFilter();

        public bool IsReusable => false;
    }
}
