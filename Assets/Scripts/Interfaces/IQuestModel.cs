using UnityEngine;

namespace MyPlatformer
{
    public interface IQuestModel
    {
        bool TryComplete(GameObject activator);
    }
}
