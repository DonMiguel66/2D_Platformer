using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class EmitterBulletController 
    {
        private List<BulletController> _bullets = new List<BulletController>();
        private Transform _transform;

        private int _currentIndex;
        private float _shotCooldown;

        private float _delay = 1f;
        private float _startSpeed = 10f;

        public EmitterBulletController(List<LevelObjectView> bulletViews, Transform transform)
        {
            _transform = transform;
            foreach (LevelObjectView BulletView in bulletViews )
            {
                _bullets.Add(new BulletController(BulletView));
            }
        }
        
        public void Execute()
        {
            if(_shotCooldown >0)
            {
                //_bullets[_currentIndex].Active(false);
                _shotCooldown -= Time.deltaTime;

            }
            else
            {
                _shotCooldown = _delay;
                _bullets[_currentIndex].Shot(_transform.position, _transform.up*_startSpeed);
                _currentIndex++;
                if(_currentIndex >=_bullets.Count)
                {
                    _currentIndex = 0;
                }
            }
        }
    }
}