namespace SecondProject.Movement
{
    public interface ISpeedModifier
    {
        float GetSpeedMultiplier();
        bool IsActive();
    }
}
