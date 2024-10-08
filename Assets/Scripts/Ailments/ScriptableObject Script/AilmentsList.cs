﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Mods
{
    [CreateAssetMenu(fileName = "NewAilmentList", menuName = "Mods/Ailment_Ref/MasterList")]
    public class AilmentsList : ScriptableObject
    {
        private ModifierManager modManager;

        // Drag and drop all new ailments in the field below
        [SerializeField] private List<Ailment> ailmentsList_Template;
        [HideInInspector]
        public List<Ailment> ailmentsList;

        #region SETUP
        /* ********
         * Creates instances of Ailment SO then adds them to a List 
         * Used for triggering/disabling ailments without affecting SO (data) values
         * ********/
        public void Initialize(ModifierManager modMgr)
        {
            modManager = modMgr;
            foreach (Ailment ailment in ailmentsList_Template)
            {
                ailmentsList.Add(Instantiate(ailment));
            }
        }
        #endregion

        public void InitializeAilment(Ailment ailmentToFind, StatEffect statEffect)
        {
            for (int i = ailmentsList.Count - 1; i >= 0; i--)
            {
                if (ailmentToFind.ailmentName == ailmentsList[i].ailmentName)
                {
                    ailmentsList[i].InitializeAilment(modManager, statEffect);
                }
            }
        }

        public void RemoveAilment(Ailment ailmentToFind, StatEffect statEffect)
        {
            for (int i = ailmentsList.Count - 1; i >= 0; i--)
            {
                if (ailmentToFind.ailmentName == ailmentsList[i].ailmentName)
                {
                    ailmentsList[i].RemoveAilment(modManager, statEffect);
                }
            }
        }

        public void Tick()
        {
            if (ailmentsList == null) { Debug.LogWarning("No ailments found in " + ailmentsList_Template); return; }
            for (int i = ailmentsList.Count - 1; i >= 0; i--)
            {
                ailmentsList[i].Tick(modManager);
            }
        }

        public bool AilmentActive(Ailment ailmentToFind)
        {
            foreach (Ailment ailment in ailmentsList)
            {
                if (ailmentToFind.ailmentName == ailment.ailmentName)
                {
                    return ailment.IsActive;
                }
            }
            return false;
        }

    }
}