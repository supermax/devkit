using DevKit.Core.Observables;

namespace DevKit.Nexus.Demo
{
    public class TestModel : Observable
    {
        private string _text = "Hello World!";

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                {
                    return;
                }

                var prevValue = _text;
                _text = value;
                base.InvokePropertyChanged(nameof(Text), prevValue, _text);
            }
        }
    }
}
