using UnityEngine;

namespace DevKit.Core.Objects
{
    public static class MonoBehaviourRoot
    {
        private const string SingletonsRootName = "[SINGLETONS]";

        private static GameObject _root;

        internal static GameObject GetRoot()
        {
            // Create root game object for "[SINGLETONS]"
            if (_root != null)
            {
                return _root;
            }

            var root = GameObject.Find(SingletonsRootName);
            if (root == null)
            {
                root = new GameObject(SingletonsRootName)
                    {
                        isStatic = true
                    };
                if (Application.isPlaying)
                {
                    Object.DontDestroyOnLoad(root);
                }
            }
            _root = root;
            return _root;
        }

		public static void ResetAll()
		{
			Object.Destroy(_root);
		}
    }
}
