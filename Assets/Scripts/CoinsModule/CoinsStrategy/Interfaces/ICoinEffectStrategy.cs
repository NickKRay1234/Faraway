namespace Factories
{
    public interface ICoinEffectStrategy
    {
        void ApplyEffect(Player player);
        void CancelEffect();
    }
}