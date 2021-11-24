using UnityEngine;

namespace MyPlatformer
{
    public class BulletController
    {
        private Vector3 _velocity;
        private LevelObjectView _view;

        private float _angle;
        private Vector3 _axis;

        public BulletController(LevelObjectView view)
        {
            _view = view;
            Active(false);
        }

        public void Active(bool val)
        {
            _view.gameObject.SetActive(val);
        }

        public void SetVelocity(Vector3 velocity)
        {
            _velocity = velocity;
            _angle = Vector3.Angle(Vector3.left, _velocity);
            _axis = Vector3.Cross(Vector3.left, _velocity);
            _view.transform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }

        public void Shot(Vector3 position, Vector3 velocity)
        {
            Active(true);
            SetVelocity(velocity);
            _view.transform.position = position;
            _view._rigidbody.velocity = Vector2.zero;
            _view._rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}