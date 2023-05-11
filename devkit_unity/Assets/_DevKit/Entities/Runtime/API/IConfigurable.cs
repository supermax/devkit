using System;

namespace DevKit.Entities.API
{
    public interface IConfiguration
    {
        string Version { get; }

        DateTime? UpdateTime { get; }
    }
}
