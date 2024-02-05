namespace Factories
{
    public interface IProduct
    {
        public string ProductName { get; set; }
        public void Initialize(ICoinEffectStrategy effectStrategy);
    }
}