namespace WorkScheduler.Modes
{
    public interface IMode
    {
        string Name { get; }
        string Desc { get; }
        void Run();
    }
}
