using System;

namespace CMMMobileMaui.API.Contracts.Models.COMMON
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class CanSortItemAttribute : Attribute
    {
        public CanSortItemAttribute() { }
    }
}
