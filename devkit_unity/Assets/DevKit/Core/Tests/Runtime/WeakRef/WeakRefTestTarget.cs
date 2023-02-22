using System.Diagnostics;

namespace DevKit.Tests.Core.WeakRef
{
    public class WeakRefTestTarget
    {
        public void TestVoidCallback()
        {
            Debug.WriteLine("{0} invoked", nameof(TestVoidCallback));
        }
    }
}
