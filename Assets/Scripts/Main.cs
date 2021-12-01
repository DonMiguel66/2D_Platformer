using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        //[SerializeField] private CollectableAnimatorConfig _coinConfig;
        [SerializeField] private SpriteAnimatorConfig _coinConfig;
        [SerializeField] private int _animationSpeed = 10;
        [SerializeField] private float _playerSpeed = 100;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private TurrelView _turrelView;
        [SerializeField] private List<LevelObjectView> _coinsViews;
        [SerializeField] private GeneratorLevelView _generatorLevelView;


        private SpriteAnimatorController _playerAnimator;
        private SpriteAnimatorController _coinAnimator;
        private CameraController _cameraController;
        private PlayerController _playerController;
        private EmitterBulletController _emitterBulletController;
        private TurrelController _turrelController;
        private CoinsController _coinsController;
        private GeneratorController _generatorController;


        void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _coinConfig = Resources.Load<SpriteAnimatorConfig>("CoinAnimCfg");

            if (_playerConfig)
            {
                _playerAnimator = new SpriteAnimatorController(_playerConfig);
            }

            if (_coinConfig)
            {
                _coinAnimator = new SpriteAnimatorController(_coinConfig);
            }

            _playerAnimator.StartAnimation(_playerView._spriteRenderer, PlayerAnimState.Run, true);

            _playerController = new PlayerController(_playerView, _playerAnimator, _playerSpeed);

            _cameraController = new CameraController(_playerView.transform, Camera.main.transform);

            _turrelController = new TurrelController(_turrelView._muzzleTransform, _playerView._transform);
            _emitterBulletController = new EmitterBulletController(_turrelView._bullets, _turrelView._emitterTransform);
            _coinsController = new CoinsController(_playerView, _coinsViews, _coinAnimator);

            _generatorController = new GeneratorController(_generatorLevelView);
            _generatorController.Init();
        }

        void Update()
        {
            _playerController.Execute();
            _cameraController.Execute();
            _turrelController.Execute();
            _emitterBulletController.Execute();
            _coinAnimator.Execute();
        }
    }
}