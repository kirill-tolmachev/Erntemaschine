namespace Erntemaschine.Controllers
{
    internal class ResourcesStorage
    {
        public float X { get; set; }

        public float W { get; set; }

        public void Clear()
        {
            X = 0;
            W = 0;
        }
    }
}