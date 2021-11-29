using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PatrolAIModel
    {
        private readonly Transform[] _waypoints;
        private int _currentWPIndex;
        
        public PatrolAIModel(Transform[] waypoints)
        {
            _waypoints = waypoints;
            _currentWPIndex = 0;
        }

        public Transform GetNextTarget()
        {
            if (_waypoints == null) return null;
            _currentWPIndex = (_currentWPIndex + 1) % _waypoints.Length;
            return _waypoints[_currentWPIndex];
            Debug.Log("Target reached");
        }

        public Transform GetClosestTarget(Vector2 fromPosition)
        {
            if (_waypoints == null) return null;
            var closestIndex = 0;
            var closestDistance = 0.0f;
            for (var i = 0; i < _waypoints.Length; i++)
            {
                var distance = Vector2.Distance(fromPosition, _waypoints[i].position);
                if (closestDistance > distance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            _currentWPIndex = closestIndex;
            return _waypoints[_currentWPIndex];

        }
    }
}