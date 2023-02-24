#region

using DevKit.Logging;
using DevKit.Logging.Extensions;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace DevKit.PubSub.Demo
{
    public class ChatMsgCountController : MonoBehaviour
    {
        [SerializeField] private Text _counterText;

        private int _msgCount;

        private void Awake()
        {
            Debug.AssertFormat(_counterText != null, "{0} is not assigned", nameof(_counterText));
        }

        private void Start()
        {
            Messenger.Default
                     .Subscribe<ChatPayload>(OnChatMessageReceived, ChatMessageFilter);
        }

        private bool ChatMessageFilter(ChatPayload payload)
        {
            var accepted = payload != null && _counterText != null;
            return accepted;
        }

        private void OnDestroy()
        {
            Loggers.Console.LogInfo("{0} destroyed", nameof(ChatMsgCountController));
        }

        private void OnChatMessageReceived(ChatPayload payload)
        {
            this.LogInfo("Received: {0}", payload.ToString());
            _counterText.text = $"Message Count: {++_msgCount}";
        }

        public void KillMe()
        {
            this.LogInfo("Killing {0}", gameObject.ToString());
            Destroy(gameObject);
        }
    }
}
