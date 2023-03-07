using UnityEngine;

namespace DevKit.PubSub.Components
{
    /// <summary>
    /// Interface for Payload Command
    /// </summary>
    public interface IPayloadCommand
    {
        string Id { get; set; }

        ScriptableObject Data { get; set; }
    }
}
