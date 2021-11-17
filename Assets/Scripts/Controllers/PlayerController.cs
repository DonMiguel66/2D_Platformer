using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{ 
    public class PlayerController
    {
        private float _xAxisInput;
        private bool _isJump;
        
        private float _playerSpeed = 3f;
        private float _movingTreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _isMoving = false;

        private float _jumpSpeed = 5f;
        private float _jumpTreshold = 1f;
        private float _gravityForce = -9.8f;
        private float _groundLevel = 0.5f;
        private bool _isDoubleJump = false;
        private float _yVelocity;
        private float _xVelocity;

        private LevelObjetView _playerView;
        private SpriteAnimatorController _playerAnimator;

        public PlayerController(LevelObjetView player, SpriteAnimatorController animator)
        {
            _playerView = player;
            _playerAnimator = animator;
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Idle, true);
        }

        private void MoveTowards()
        {
            _playerView._transform.position += Vector3.right * (Time.deltaTime * _playerSpeed * (_xAxisInput < 0 ? -1 : 1));
            _playerView._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        public bool IsGrounded()
        {
            return _playerView._transform.position.y <= _groundLevel && _yVelocity <= 0;
        }

        public void Execute()
        {
            _playerAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            //_isJump = Input.GetAxis("Vertical") > 0;
            _isJump = Input.GetButtonDown("Jump");
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;

            if(_isMoving)
            {
                MoveTowards();
            }

            if (IsGrounded())
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true);
                if (_isJump && _yVelocity <= 0)
                {
                    _yVelocity = _jumpSpeed;
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = float.Epsilon;
                    _playerView._transform.position = _playerView._transform.position.Change(y: _groundLevel);
                    _isDoubleJump = false;
                }
            }
            else
            {
                if (_isJump && Mathf.Abs(_yVelocity) > 0.15 && _isDoubleJump == false)
                {
                    _yVelocity = _jumpSpeed * 0.75f;
                    _isDoubleJump = true;
                }
                if(Mathf.Abs(_yVelocity)>_jumpTreshold)
                {
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isDoubleJump ? AnimState.DoubleJump : AnimState.Jump, true);
                }
                _yVelocity += _gravityForce * Time.deltaTime;
                _playerView._transform.position += Vector3.up * (Time.deltaTime * _yVelocity);
            }
        }
    }
}