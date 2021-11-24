using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class CoinsController : IDisposable
    {
        private float _animationSpeed;

        private LevelObjectView _playerView;
        private SpriteAnimatorController _coinAnimator;
        private List<LevelObjectView> _coinsViews;

        public CoinsController(LevelObjectView player, List<LevelObjectView> coins, SpriteAnimatorController coinAnimator)
        {
            _playerView = player;
            _coinAnimator = coinAnimator;
            _coinsViews = coins;

            _playerView.OnLevelObjectContact += OnLevelObjectContact;

            foreach (LevelObjectView coinView in _coinsViews)
            {
                _coinAnimator.StartAnimation(coinView._spriteRenderer, PlayerAnimState.Idle, true);
            }
        }
        private void OnLevelObjectContact(LevelObjectView contacView)
        {
            if (_coinsViews.Contains(contacView))
            {
                _coinAnimator.StopAnimaion(contacView._spriteRenderer);
                GameObject.Destroy(contacView.gameObject);
            }
        }
        public void Dispose()
        {
            _playerView.OnLevelObjectContact -= OnLevelObjectContact;
        }
    }
}