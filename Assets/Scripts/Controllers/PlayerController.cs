using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{ 
    public class PlayerController
    {
        private float _xAxisInput;
        private bool _isJump;
        
        private float _playerSpeed;
        private float _movingTreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _isMoving = false;

        private float _jumpSpeed = 5f;
        private float _jumpTreshold = 1f;
        private bool _isDoubleJump = false;
        private float _yVelocity;
        private float _xVelocity;

        private LevelObjectView _playerView;
        private SpriteAnimatorController _playerAnimator;
        private readonly ContactPooler _contactPooler;

        public PlayerController(LevelObjectView player, SpriteAnimatorController animator, float playerSpeed)
        {
            _playerView = player;
            _playerSpeed = playerSpeed;
            _playerAnimator = animator;
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, PlayerAnimState.Idle, true);
            _contactPooler = new ContactPooler(_playerView._collider);
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _playerSpeed * (_xAxisInput < 0 ? -1 : 1);

            _playerView._rigidbody.velocity = _playerView._rigidbody.velocity.Change(x: _xVelocity);
            _playerView._transform.localScale = (_xAxisInput < 0 ? _leftScale : _rightScale);
        }

        private void Jump(float jumpSpeed)
        {
            _playerView._rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }


        public void Execute()
        {
            _contactPooler.Execute();
            _playerAnimator.Execute();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetButtonDown("Jump");
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;
            float vertivalVelocity = _playerView._rigidbody.velocity.y;

            if (_isMoving)
            {
                MoveTowards();
            }

            if (_contactPooler.IsGrounded)
            {
                _isDoubleJump = false;
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? PlayerAnimState.Run : PlayerAnimState.Idle, true);
                if (_isJump && Mathf.Abs(vertivalVelocity) <= _jumpTreshold)
                {
                    Jump(_jumpSpeed);
                }
            }
            else
            {
                if (_isJump && Mathf.Abs(vertivalVelocity) > 0.3f && _isDoubleJump == false)
                {
                    if(vertivalVelocity < -0.5)
                        Jump(_jumpSpeed * 1.5f);
                    else
                        Jump(_jumpSpeed * 0.75f);
                    _isDoubleJump = true;
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, PlayerAnimState.DoubleJump, true);
                }
                else if(Mathf.Abs(vertivalVelocity) > _jumpTreshold && vertivalVelocity <-0.15)
                {
                    //_playerAnimator.StartAnimation(_playerView._spriteRenderer, _isDoubleJump ? AnimState.DoubleJump : AnimState.Jump, true);
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, PlayerAnimState.Jump, true);
                }
            }
        }
    }
}