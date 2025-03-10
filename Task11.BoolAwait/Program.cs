using System.Diagnostics;
using System.Runtime.CompilerServices;

var sw = Stopwatch.StartNew();

await (await await false & await true);

Console.WriteLine($"END: {sw.ElapsedMilliseconds}");

public static class BoolExtensions {
    public static BoolAwaiter GetAwaiter(this bool value)
    {
        return new BoolAwaiter(value);
    }
}

public class BoolAwaiter : INotifyCompletion
{
    public bool IsCompleted => _completed;

    public BoolAwaiter(bool value)
    {
        _value = value;
        var timer = new Timer(SetCompleted, null, 500, -1);        
    }

    public void OnCompleted(Action continuation)
    {
        _continuation = continuation;
    }

    public bool GetResult() => _value;

    private void SetCompleted(object? state)
    {
        _completed = true;
        _continuation();
    }

    private readonly bool _value;
    private bool _completed;
    private Action _continuation;
}