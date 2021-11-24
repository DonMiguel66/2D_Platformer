using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform _transform;
        public SpriteRenderer _spriteRenderer;
        public Collider2D _collider;
        public Rigidbody2D _rigidbody;

        public Action<LevelObjectView> OnLevelObjectContact { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            LevelObjectView levelObject = collision.gameObject.GetComponent<LevelObjectView>();
            OnLevelObjectContact?.Invoke(levelObject);
        }
    }
}