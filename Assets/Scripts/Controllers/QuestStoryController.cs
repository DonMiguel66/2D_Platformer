using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MyPlatformer
{
    public class QuestStoryController : IQuestStory
    {
        private List<IQuest> _questCollection = new List<IQuest>();

        public bool IsDone => _questCollection.All(value => value.IsCompleted);

        public QuestStoryController(List<IQuest> quests)
        {
            _questCollection = quests;
            Subscribe();
            ResetQuest(0);
        }
        private void OnQuestCompleted(object senger, IQuest quest)
        {
            int index = _questCollection.IndexOf(quest);
            if(IsDone)
            {
                Debug.Log("Story is done!");
            }
            else
            {
                ResetQuest(++index);
            }    
        }
        private void Subscribe()
        {
            foreach (IQuest quest in _questCollection)
            {
                quest.Completed += OnQuestCompleted;
            }
        }

        private void Unsubscribe()
        {
            foreach (IQuest quest in _questCollection)
            {
                quest.Completed += OnQuestCompleted;
            }
        }

        private void ResetQuest(int index)
        {
            if (index < 0 || index > _questCollection.Count)
                return;

            IQuest nextQuest = _questCollection[index];

            if(nextQuest.IsCompleted)
            {
                OnQuestCompleted(this, nextQuest);
            }
            else
            {
                _questCollection[index].Reset();
            }    
        }

        public void Dispose()
        {
            Unsubscribe();
            foreach  (IQuest quest in _questCollection)
            {
                quest.Dispose();
            }
        }
    }
}
