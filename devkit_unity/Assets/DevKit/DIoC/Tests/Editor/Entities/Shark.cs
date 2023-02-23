using DevKit.DIoC.Attributes;

namespace DevKit.Editor.IOC.Tests.Entities
{
    public class Shark : Fish
    {
        [Inject]
        public IFood Food { get; set; }
    }
}
