﻿namespace Application.Extensions
{
    public static class IntExtensions
    {
        public static TimeSpan Minutes(this int @int) =>
            TimeSpan.FromMinutes(@int);
    }
}
