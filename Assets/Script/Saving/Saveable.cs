namespace RPG.Saving
{
    public interface Saveable
    {
        object captureState();
        void restoreState(object state);
    }
}