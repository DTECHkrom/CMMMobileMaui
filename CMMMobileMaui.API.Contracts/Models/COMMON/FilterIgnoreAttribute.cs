using System;

namespace CMMMobileMaui.API.Contracts.Models.COMMON
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    internal class FilterIgnoreAttribute : Attribute
    {
        public FilterIgnoreAttribute() { }
    }
}
