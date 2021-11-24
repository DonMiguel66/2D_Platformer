using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public enum CoinAnimState
    {
        Rotation = 0
    }

    [CreateAssetMenu(fileName = "CollectableAnimatorCfg", menuName = "Configs / Collectable Animation Cfg", order = 2)]
    public class CollectableAnimatorConfig : ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public CoinAnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
            public float AnimationSpeed;
        }

        public List<SpriteSequence> Sequence = new List<SpriteSequence>();

    }
}
