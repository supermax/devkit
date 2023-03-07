using DevKit.DIoC.Attributes;

namespace DevKit.DIoC.Tests.Editor.Entities
{
    public class Dog : Mammal
    {
        [Inject]
        public IFood Food { get; set; }
    }
}
