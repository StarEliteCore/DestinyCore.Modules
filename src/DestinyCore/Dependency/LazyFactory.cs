using Microsoft.Extensions.DependencyInjection;
using System;

namespace DestinyCore.Dependency
{
    public sealed class LazyFactory<T> : Lazy<T> where T : class
    {
        public LazyFactory(IServiceProvider provider)
            : base(provider.GetRequiredService<T>)
        {
        }
    }
}