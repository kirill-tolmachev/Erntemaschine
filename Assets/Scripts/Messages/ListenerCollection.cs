using System;

namespace Assets.Scripts.Messages
{
    internal class ListenerCollection<TMessage> : ListenerCollectionBase<Action<TMessage>>
    {
    }
}