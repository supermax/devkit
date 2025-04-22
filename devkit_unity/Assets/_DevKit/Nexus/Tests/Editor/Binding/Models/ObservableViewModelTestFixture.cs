using DevKit.Core.Observables;
using DevKit.Nexus.MVVM;

namespace DevKit.Nexus.Tests.Editor.Binding.Models
{
    public class ObservableViewModelTestFixture : BaseViewModel
    {
        public ObservableModel ChildModel { get; set; } = new();

        public ObservableCollection<string> List { get; set; } = new();

        // TODO implement test for this property
        public ObservableCollection<string, string> Dictionary { get; set; } = new();
    }
}
