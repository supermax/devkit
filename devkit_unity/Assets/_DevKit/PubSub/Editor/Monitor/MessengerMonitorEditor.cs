#if UNITY_EDITOR
using UnityEditor;

namespace DevKit.PubSub.Monitor
{
    [CustomEditor(typeof(MessengerMonitor))]
    public class MessengerMonitorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var monitor = (MessengerMonitor)target;
            // TODO draw subscribers and etc

            base.OnInspectorGUI();
        }
    }
}
#endif
