using UnityEngine;

namespace Zephyr.UI
{
    [CreateAssetMenu(fileName = "New UI Effect MasterList", menuName = "UI/MasterList")]
    public class UIStatEffectMasterList_SO : ScriptableObject
    {
        [Tooltip("Don't forget to add all new images here!")]
        public UIStatEffect_SO[] _statEffectImages;

        public UIStatEffect_SO[] ImageArray { get { return _statEffectImages; } }
    }
}