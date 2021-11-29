using UnityEngine;

namespace MyPlatformer
{
    public interface IProtector
    {
        void StartProtection(GameObject invader);
        void FinishProtection(GameObject invader);
    }
}
