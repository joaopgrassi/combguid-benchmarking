using System;

namespace ConsoleApp.Entities
{
    public abstract class BaseBenchmarkEntity
    {
        public Guid Id { get; set; }

        public Guid AnotherId { get; set; }

        public string Value { get; set; }
    }
}
