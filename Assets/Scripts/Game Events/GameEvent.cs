using System.Collections.Generic;
using UnityEngine;
using Zephyr.UI;

namespace Zephyr.Events
{
    [CreateAssetMenu(fileName = "NewEvent", menuName = "Game Event/Event")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void Raise(UIStatEffect_SO effect_SO)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(effect_SO);
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }

    }
}