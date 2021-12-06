using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class CoinQuestModel : IQuestModel
    {
        private const string TargetTag = "Player";
        public bool TryComplete(GameObject activator)
        {
            return activator.CompareTag(TargetTag);
        }
    }
}
