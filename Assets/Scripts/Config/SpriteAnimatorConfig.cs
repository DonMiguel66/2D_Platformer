using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{    
    public enum PlayerAnimState
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
        DoubleJump = 3
    }


    [CreateAssetMenu(fileName ="CharachterSpriteAnimatorCfg", menuName = "Configs / Animation Cfg", order = 1)]
    public class SpriteAnimatorConfig : ScriptableObject
    {   
        [Serializable]
        public sealed class SpriteSequence 
        {
            public PlayerAnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
            public float AnimationSpeed;
        }

        public List<SpriteSequence> Sequence = new List<SpriteSequence>();
    }

}
