using Assets.Scripts.Messages;

namespace Erntemaschine.Messages
{
    internal class RewardGranted : IMessage
    {
        public float Value { get; }

        public RewardGranted(float value)
        {
            Value = value;
        }
    }
}
