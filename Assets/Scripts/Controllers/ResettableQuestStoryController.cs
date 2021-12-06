using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MyPlatformer
{
    public class ResettableQuestStoryController : IQuestStory
    {
        private readonly List<IQuest> _questCollection = new List<IQuest>();

        public bool IsDone => _questCollection.All(value => value.IsCompleted);

        private int _currentIndex;

        public ResettableQuestStoryController(List<IQuest> quests)
        {
            _questCollection = quests;
            Subscribe();
            ResetQuest();
        }
        private void OnQuestCompleted(object senger, IQuest quest)
        {
            int index = _questCollection.IndexOf(quest);
            if (_currentIndex == index)
            {
                _currentIndex++;
                if(IsDone)
                    Debug.Log("Story is done!");
            }
            else
            {
                ResetQuest();
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

        private void ResetQuest()
        {
            _currentIndex = 0;
            foreach (IQuest quest in _questCollection)
            {
                quest.Reset();
            }
        }

        public void Dispose()
        {
            Unsubscribe();
            foreach (IQuest quest in _questCollection)
            {
                quest.Dispose();
            }
        }
    }
}
