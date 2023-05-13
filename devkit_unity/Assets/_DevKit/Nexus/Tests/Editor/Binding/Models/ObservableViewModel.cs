using DevKit.Nexus.MVVM;

namespace DevKit.Nexus.Tests.Editor.Binding.Models
{
    public class ObservableViewModel : BaseViewModel
    {
        public ObservableViewModel ChildModel { get; } = new();
    }
}
