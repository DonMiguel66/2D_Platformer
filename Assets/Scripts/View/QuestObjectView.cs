using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class QuestObjectView : LevelObjectView
    {
        [SerializeField] private int _id;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _completedColor;
        public int Id { get => _id; set => _id = value; }

        private void Awake()
        {
            _defaultColor = _spriteRenderer.color;
        }

        public void SetComplete()
        {
            _spriteRenderer.color = _completedColor;
        }

        public void SetDefault()
        {
            _spriteRenderer.color = _defaultColor;
        }
    }
}
