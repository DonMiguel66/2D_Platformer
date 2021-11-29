using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class EnemiesConfigurator : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _protectorAIView;
        [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
        [SerializeField] private AIPatrolPath _protectorAIPatrolPath;
        [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
        [SerializeField] private Transform[] _protectorWaypoints;

        private ProtectorAIController _protectorAI;
        private ProtectedZoneController _protectedZone;

        void Start()
        {

            _protectorAI = new ProtectorAIController(_protectorAIView, new PatrolAIModel(_protectorWaypoints), _protectorAIDestinationSetter, _protectorAIPatrolPath);
            _protectorAI.Init();

            _protectedZone = new ProtectedZoneController(_protectedZoneTrigger, new List<ProtectorAIController> { _protectorAI });
            _protectedZone.Init();

        }
        private void OnDestroy()
        {
            _protectorAI.DeInit();
            _protectedZone.DeInit();
        }

    }
}
