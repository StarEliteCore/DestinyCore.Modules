using MediatR;

namespace DestinyCore.Events
{
    public abstract class NotificationHandlerBase<TEvent> : EventHandlerBase<TEvent>, INotificationHandler<TEvent> where TEvent : EventBase
    {

    }
}
