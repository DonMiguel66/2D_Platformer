using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private float _playerSpeed = 100;
        [SerializeField] private LevelObjetView _playerView;

        private SpriteAnimatorController _playerAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;

        void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            if(_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
            }

            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Run, true);

            _playerController = new PlayerController(_playerView, _playerAnimator, _playerSpeed);

            _cameraController = new CameraController(_playerView.transform, Camera.main.transform);
        }

        void Update()
        {
            _playerController.Execute();
            _cameraController.Execute();
        }
    }
}