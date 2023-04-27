namespace DevKit.PubSub.API
{
    public interface IMessengerSubscriber
    {
        //IMessenger Messenger { get; }

        IMessengerSubscriber SubscribeAll();

        IMessengerSubscriber UnsubscribeAll();
    }
}
