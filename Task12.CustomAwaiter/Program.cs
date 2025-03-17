using System.Runtime.CompilerServices;

var me = new MyAwaiter();

Console.WriteLine($"Run {Environment.CurrentManagedThreadId}");

_ = Task.Run(() =>
{
    Thread.Sleep(1000);
    Console.WriteLine($"SetCompleted {Environment.CurrentManagedThreadId}");
    me.SetCompleted();
});

await me;

Console.WriteLine($"After await {Environment.CurrentManagedThreadId} : {Thread.CurrentThread.ExecutionContext}");

class MyAwaiter:INotifyCompletion
{
    public bool IsCompleted => _completed;

    public MyAwaiter GetAwaiter() => this;

    public void GetResult() { }

    public void OnCompleted(Action continuation)
    {
        _continuation = continuation;
    }

    public void UnsafeOnCompleted(Action continuation)
    {
        _continuation = continuation;        
    }

    public void SetCompleted()
    {
        _completed = true;
        _continuation();
    }

    private Action _continuation;
    private bool _completed;
}