﻿using DestinyCore.Events.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DestinyCore.Events.EventBus
{
    public sealed class InMemoryDefaultBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryDefaultBus(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        ///  发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEventBase

        {
            return _mediator.Publish(@event);
        }


    }
}
