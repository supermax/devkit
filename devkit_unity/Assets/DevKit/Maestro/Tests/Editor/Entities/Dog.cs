using DevKit.IOC.Attributes;

namespace DevKit.Editor.IOC.Tests.Entities
{
    public class Dog : Mammal
    {
        [Inject]
        public IFood Food { get; set; }
    }
}
