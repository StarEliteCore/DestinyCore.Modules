using System;

namespace DestinyCore.Mapping
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoMapFromAttribute : AutoMappingAttribute
    {
        public override AutoMapDirection Direciton
        {
            get { return AutoMapDirection.From; }
        }
        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}
