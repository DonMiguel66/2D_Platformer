using UnityEngine;

namespace MyPlatformer
{
    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs / Quest Story Cfg", order = 1)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] quests;
        public QuestStoryType questStoryType;
    }

    public enum QuestStoryType
    {
        Common,
        Resettable
    }
}
