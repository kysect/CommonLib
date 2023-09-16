namespace Kysect.CommonLib.DependencyInjection;

public sealed class IocContainerDependencyDisposer : IDisposable
{
    private readonly Stack<IDisposable> _elements = new Stack<IDisposable>();
    private bool _disposedValue;

    public T Add<T>(T instance) where T : class, IDisposable
    {
        _elements.Push(instance);
        return instance;
    }

    private void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;

        if (disposing)
        {
            while (_elements.Count > 0)
            {
                IDisposable disposable = _elements.Pop();
                disposable.Dispose();
            }
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
    }
}