using System;


namespace MyPlatformer
{
    public interface IQuestStory : IDisposable
    {
        bool IsDone { get; }
    }
}
