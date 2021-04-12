using UnityEngine;
using UnityEngine.Events;
using Zephyr.UI;

namespace Zephyr.Events
{

    [System.Serializable]
    public class DisplayImageEvent : UnityEvent<UIStatEffect_SO> { }
    [System.Serializable]
    public class RemoveImageEvent : UnityEvent<UIStatEffect_SO> { }

    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent Response;
        public DisplayImageEvent DisplayImage;
        public RemoveImageEvent RemoveImage;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }
        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised() { Response.Invoke(); }

        public void OnEventRaised(UIStatEffect_SO effect_SO) { Response.Invoke(); }

    }
}