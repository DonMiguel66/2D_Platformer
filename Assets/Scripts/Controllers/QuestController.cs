using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class QuestController : IQuest
    {
        public event EventHandler<IQuest> Completed;
        public bool IsCompleted { get; private set; }

        private QuestObjectView _view;
        private bool _active;
        private IQuestModel _model;

        public QuestController(QuestObjectView view, IQuestModel questModel)
        {
            _view = view;
            _model = questModel;
        }

        private void OnCompleted()
        {
            Completed?.Invoke(this, this);
        }
        private void OnContact(LevelObjectView arg)
        {
            bool complete = _model.TryComplete(arg.gameObject);

            if(complete)
            {
                Complete();
            }
        }
        public void Complete()
        {
            if (!_active)
                return;
            _active = false;
            _view.OnLevelObjectContact -= OnContact;
            _view.SetComplete();
            OnCompleted();

        }
        public void Reset()
        {
            if (_active)
                return;
            _active = true;
            _view.OnLevelObjectContact += OnContact;
            _view.SetDefault();
        }
        public void Dispose()
        {
            _view.OnLevelObjectContact -= OnContact;
        }
    }
}
