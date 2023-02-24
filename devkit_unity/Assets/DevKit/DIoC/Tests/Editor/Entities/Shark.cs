using DevKit.DIoC.Attributes;

namespace DevKit.DIoC.Tests.Editor.Entities
{
    public class Shark : Fish
    {
        [Inject]
        public IFood Food { get; set; }
    }
}
