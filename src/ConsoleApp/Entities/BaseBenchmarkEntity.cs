using System;

namespace ConsoleApp.Entities
{
    public abstract class BaseBenchmarkEntity<TId> where TId : struct
    {
        public TId Id { get; set; }

        public string Value { get; set; }
    }
}
