using DevKit.Entities.API;

namespace DevKit.Entities.Tests.Engine
{
    public class EngineImplementation : Entities.Engine
    {
        public override T Create<T>()
        {
            throw new System.NotImplementedException();
        }

        public override TInterface Register<TInterface, TImplementation>()
        {
            throw new System.NotImplementedException();
        }
    }
}
