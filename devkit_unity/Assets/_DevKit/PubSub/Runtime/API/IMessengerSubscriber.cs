namespace DevKit.PubSub.API
{
    public interface IMessengerSubscriber
    {
        IMessengerSubscriber SubscribeAll();

        IMessengerSubscriber UnsubscribeAll();
    }
}
