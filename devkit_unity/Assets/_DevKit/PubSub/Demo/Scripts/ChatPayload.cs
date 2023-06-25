namespace DevKit.PubSub.Demo
{
    public class ChatPayload
    {
        public string UserId { get; set; }

        public string Text { get; set; }

        public override string ToString()
        {
            return $"{nameof(UserId)}: {UserId}, {nameof(Text)}: {Text}";
        }
    }
}
