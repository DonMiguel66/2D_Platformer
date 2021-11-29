using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class ProtectedZoneController
    {
        //private readonly List<IProtector> _protectors;
        private readonly List<ProtectorAIController> _protectors;
        private readonly LevelObjectTrigger _trigger;

        public ProtectedZoneController(LevelObjectTrigger trigger, List<ProtectorAIController> protectors)
        {
            _trigger = trigger != null ? trigger : throw new ArgumentNullException(nameof(trigger));
            _protectors = protectors != null ? protectors : throw new ArgumentNullException(nameof(protectors)); ;
        }

        public void Init()
        {
            _trigger.TriggerEnter += OnContact;
            _trigger.TriggerExit += OnExit;
        }

        private void OnContact(object sender, GameObject go)
        {
            foreach (var protector in _protectors)
            {
                protector.StartProtection(go);
            }

        }
        private void OnExit(object sender, GameObject go)
        {
            foreach (var protector in _protectors)
            {
                protector.FinishProtection(go);
            }

        }
        
        public void DeInit()
        {
            _trigger.TriggerEnter -= OnContact;
            _trigger.TriggerExit -= OnExit;
        }
    }
}