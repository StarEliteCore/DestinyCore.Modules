﻿namespace DestinyCore.Entity
{
    public class InputDto<TKey> : IInputDto<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
