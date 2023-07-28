namespace Kysect.CommonLib.DependencyInjection;

public class IocContainerDependencyDisposer : IDisposable
{
    private readonly Stack<IDisposable> _elements = new Stack<IDisposable>();

    public T Add<T>(T instance) where T : class, IDisposable
    {
        _elements.Push(instance);
        return instance;
    }

    public void Dispose()
    {
        while (_elements.Count > 0)
        {
            IDisposable disposable = _elements.Pop();
            disposable.Dispose();
        }
    }
}