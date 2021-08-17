using DestinyCore.Dependency;
using System;

namespace DestinyCore.Reflection
{
    [IgnoreDependency]
    public interface IFinder<out TItem>
    {
        TItem[] Find(Func<TItem, bool> predicate);


        TItem[] FindAll();
    }
}
