using System;

namespace DevKit.Core.Observables.API
{
    public abstract class BaseEventArgs : EventArgs
    {
        public virtual bool IsHandled { get; set; }

        public Exception Error { get; }

        protected BaseEventArgs()
        {

        }

        protected BaseEventArgs(Exception error)
        {
            Error = error;
        }
    }
}
