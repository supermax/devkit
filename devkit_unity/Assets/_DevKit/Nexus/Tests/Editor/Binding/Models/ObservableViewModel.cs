using DevKit.Core.Observables;
using DevKit.Nexus.MVVM;

namespace DevKit.Nexus.Tests.Editor.Binding.Models
{
    public class ObservableViewModel : BaseViewModel
    {
        public ObservableModel ChildModel { get; } = new();

        public ObservableCollection<string> List { get; } = new();

        public ObservableCollection<string, string> Dictionary { get; } = new();
    }
}
