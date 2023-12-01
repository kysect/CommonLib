namespace Kysect.CommonLib.Disposing;

#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
public class DisposableStack : IDisposable
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
{
    private bool _disposedValue;
    private readonly Stack<IDisposable> _disposables = new Stack<IDisposable>();

    public void Push(IDisposable value)
    {
        if (_disposedValue)
            throw new ObjectDisposedException("Value already disposed");

        _disposables.Push(value);
    }

    protected virtual void Dispose(bool disposing)
    {

        if (!_disposedValue)
        {
            if (disposing)
            {
                if (_disposables.Count > 0)
                {
                    _disposables.Pop().Dispose();
                }
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
    }
}