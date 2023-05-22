using System.Collections.Generic;

namespace DevKit.Nexus.Tests.Editor.Binding.Models
{
    public class SimpleModel
    {
        public string Name { get; set; }

        public List<string> List { get; } = new();

        public Dictionary<string, string> Dictionary { get; } = new();
    }
}
