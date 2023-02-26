using DevKit.Logging;
using DevKit.PubSub.API;

namespace DevKit.Tests.Messaging
{
    public abstract class BaseMessengerTest
    {
        protected readonly IMessenger Messenger;

        protected readonly ILogger Logger;

        protected BaseMessengerTest()
        {
            Logger = Logging.Logger.Default;
            Messenger = PubSub.Messenger.Default;
        }
    }
}
