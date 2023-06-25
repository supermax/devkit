using System;

namespace DevKit.Entities.API
{
    public interface IConfiguration
    {
        string Id { get; set; }

        string Name { get; set; }

        string Version { get; }

        DateTime? UpdateTime { get; }
    }
}
