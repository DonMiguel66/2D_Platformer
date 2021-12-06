using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace MyPlatformer
{
    public class QuestConfiguratorController
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuestController;
        private CoinQuestModel _model;

        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _questObjects;

        private List<IQuestStory> _questStories;



        public QuestConfiguratorController(QuestView questView)
        {
            _singleQuestView = questView._singleQuest;
            _model = new CoinQuestModel();

            _questStoryConfigs = questView._questStoryConfigs;
            _questObjects = questView._questObjects;
        }

        private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories =
            new Dictionary<QuestType, Func<IQuestModel>>(1);

        private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories =
            new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(2);

        private IQuest CreateQuest(QuestConfig cfg)
        {
            int questID = cfg.id;
            QuestObjectView questView = _questObjects.FirstOrDefault(value => value.Id == cfg.id);
            if(questView == null)
            {
                Debug.Log("No views");
                return null;
            }

            if(_questFactories.TryGetValue(cfg.questType, out var factory))
            {
                IQuestModel questModel = factory.Invoke();
                return new QuestController(questView, questModel);
            }

            Debug.Log("No model");
            return null;
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig cfg)
        {
            List<IQuest> quests = new List<IQuest>();
            foreach (QuestConfig questConfig in cfg.quests)
            {
                IQuest quest = CreateQuest(questConfig);
                if (quest == null)
                    continue;
                quests.Add(quest);
                Debug.Log("Quest added");

            }
            return _questStoryFactories[cfg.questStoryType].Invoke(quests); 
        }
        public void Init()
        {
            _singleQuestController = new QuestController(_singleQuestView, _model);
            _singleQuestController.Reset();

            _questStoryFactories.Add(QuestStoryType.Common, questCollection => new QuestStoryController(questCollection));
            _questStoryFactories.Add(QuestStoryType.Resettable, questCollection => new ResettableQuestStoryController(questCollection));

            _questFactories.Add(QuestType.Coins, () => new CoinQuestModel());

            _questStories = new List<IQuestStory>();

            foreach  (QuestStoryConfig questStoryConfig in _questStoryConfigs)
            {
                _questStories.Add(CreateQuestStory(questStoryConfig));
            }

        }
    }
}
