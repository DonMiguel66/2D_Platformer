using UnityEngine;

namespace MyPlatformer
{
    public class TurrelController 
    {
        private Transform _muzzleTransform;
        private Transform _targetTransform;

        private Vector3 _dir;
        private float _angle;
        private Vector3 _axis;

        public TurrelController(Transform muzzleTransform, Transform playerTransform)
        {
            _muzzleTransform = muzzleTransform;
            _targetTransform = playerTransform;
        }


        public void Execute()
        {
            _dir = _targetTransform.position - _muzzleTransform.position;
            _angle = Vector3.Angle(Vector3.up, _dir);
            //Взял противоположный вектор из-за того, что пушка собрана немного по-другому) Но работает всё верно)
            _axis = Vector3.Cross(Vector3.up, _dir);
            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);

        }
    }
}