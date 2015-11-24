namespace Jal.Bootstrapper.Interface
{
    public interface IBootstrapper<out T> : IBootstrapper
    {
        T Result { get; }
    }

    public interface IBootstrapper
    {
        void Configure();
    }
}
