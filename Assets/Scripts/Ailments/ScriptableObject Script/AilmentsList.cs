using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewAilmentList", menuName = "Mods/Ailment/MasterList")]
    public class AilmentsList : ScriptableObject
    {
        private ModifierManager modManager;

        [SerializeField] private List<Ailment> ailmentsList_Template;
        [SerializeField] private List<Ailment> ailmentsList; // TODO (cleanup): remove SerializeField

        public void Initialize(ModifierManager modMgr)
        {
            modManager = modMgr;
            foreach (Ailment ailment in ailmentsList_Template)
            {
                ailmentsList.Add(Instantiate(ailment));
            }
        }

        public void InitializeAilment(Ailment ailmentToFind, StatEffect statEffect)
        {
            //TODO (Ailment): Target the effect properly!!
            foreach (Ailment ailment in ailmentsList)
            {
                if (ailmentToFind.ailmentName == ailment.ailmentName)
                {
                    ailment.InitializeAilment(modManager, statEffect);
                }
            }
            //ailmentsList.Find(x => ailmentToFind).InitializeAilment(modManager, statEffect);
        }

        public void RemoveAilment(Ailment ailmentToFind)
        {
            //TODO (Ailment): Target the effect properly!!
            foreach (Ailment ailment in ailmentsList)
            {
                if (ailmentToFind.ailmentName == ailment.ailmentName)
                {
                    ailment.RemoveAilment();
                }
            }
            //ailmentsList.Find(x => ailmentToFind).RemoveAilment();
        }

        public void Tick()
        {
            if (ailmentsList == null) { Debug.LogWarning("No ailments found in " + ailmentsList_Template); return; }
            foreach (Ailment ailment in ailmentsList)
            {
                ailment.Tick();
            }
        }

    }
}