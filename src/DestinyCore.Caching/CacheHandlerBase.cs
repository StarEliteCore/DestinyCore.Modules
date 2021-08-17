using DestinyCore.Events;

namespace DestinyCore.Caching
{
    public abstract class CacheHandlerBase<TEvent> : NotificationHandlerBase<TEvent> where TEvent : EventBase
    {
        protected readonly ICache _cache = null;


        public CacheHandlerBase(ICache cache)
        {
            _cache = cache;

        }
    }
}
