using DevKit.Nexus.MVVM;
using DevKit.Nexus.UI.Binding;

namespace DevKit.Nexus.Demo
{
    public class TestViewModel : BaseComponentBinding
    {
        public TestModel Data { get; set; } = new();
    }
}
