using System;

namespace DestinyCore.Mapping
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoMapToAttribute : AutoMappingAttribute
    {
        public override AutoMapDirection Direciton
        {
            get { return AutoMapDirection.To; }
        }
        public AutoMapToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}
