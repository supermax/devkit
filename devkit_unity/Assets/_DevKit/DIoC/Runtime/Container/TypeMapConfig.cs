using System;

namespace DevKit.DIoC.Container
{
    internal class TypeMapConfig : IDisposable
    {
        public Type Type { get; set; }

        public bool IsSingleton { get; set; }

        public void Dispose()
        {
            Type = null;
        }
    }
}
