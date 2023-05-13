namespace DevKit.Nexus.Tests.Editor.Binding.Models
{
    public class ObservableModel : Core.Observables.Observable
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }
                var oldValue = _name;
                _name = value;
                InvokePropertyChanged(nameof(Name), oldValue, value);
            }
        }

        public ObservableModel()
        {
            IsUpdateSuspended = false;
        }
    }
}
