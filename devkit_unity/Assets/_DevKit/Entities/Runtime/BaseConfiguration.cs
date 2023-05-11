using System;
using DevKit.Entities.API;

namespace DevKit.Entities
{
    public abstract class BaseConfiguration : IConfiguration
    {
        public virtual string Version { get; set; } = "1.0.0.0";

        public virtual DateTime? UpdateTime { get; set; }

        public virtual string Name { get; set; }
    }
}
