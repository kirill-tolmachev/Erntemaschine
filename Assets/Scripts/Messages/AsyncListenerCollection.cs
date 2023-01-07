using System;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Messages
{
    internal class AsyncListenerCollection<TMessage> : ListenerCollectionBase<Func<TMessage, UniTask>>
    {
    }
}