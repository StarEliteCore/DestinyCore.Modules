using System.Threading;
using System.Threading.Tasks;

namespace DestinyCore.Events.Abstractions
{
    public interface IEventHandlerBase<in TEvent> where TEvent : class, IEventBase
    {

        Task Handle(TEvent notification, CancellationToken cancellationToken);

    }
}
