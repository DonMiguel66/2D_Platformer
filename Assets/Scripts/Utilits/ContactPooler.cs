using UnityEngine;

namespace MyPlatformer 
{ 
    public class ContactPooler 
    {
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];

        private const float _collisionTreshhold = 0.6f;
        private int _contactCount;
        private Collider2D _collider2D;

        public bool IsGrounded { get; private set; }
        public bool HasLeftContact { get; private set; }
        public bool HasRightContact { get; private set; }
        public bool HasTopContact { get; private set; }

        public ContactPooler(Collider2D collider)
        {
            _collider2D = collider;
        }


        public void Execute()
        {
            IsGrounded = false;
            HasLeftContact = false;
            HasRightContact = false;
            HasTopContact = false;

            _contactCount = _collider2D.GetContacts(_contacts);

            for (int i=0; i <_contactCount; i++)
            {
                if (_contacts[i].normal.y > _collisionTreshhold)
                    IsGrounded = true;
                if (_contacts[i].normal.x > _collisionTreshhold)
                    HasLeftContact = true;
                if (_contacts[i].normal.x > -_collisionTreshhold)
                    HasRightContact = true;
                if (_contacts[i].normal.y > -_collisionTreshhold)
                    HasTopContact = true;
            }
            
        }
    }
}