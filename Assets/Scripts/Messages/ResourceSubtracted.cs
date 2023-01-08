using Assets.Scripts.Messages;

namespace Erntemaschine.Messages
{
    internal class ResourceSubtracted : IMessage
    {
        public float X { get; }
        public float W { get; }

        public ResourceSubtracted(float x, float w)
        {
            X = x;
            W = w;
        }
    }
}