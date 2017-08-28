﻿using System;
using Core.Domain.Assets;
using Core.Domain.Currencies;

namespace Core.Domain.Position
{
    public abstract class BasePosition<T> : BaseEntity<T> where T : BaseEntity<T>, new()
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public Asset Asset { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
