var task = Task.Run(async () =>
{
    await Task.Delay(3000);
    return 1234;
});

using var cts = new CancellationTokenSource();

_ = Task.Run(async () =>
{
    await Task.Delay(1000);
    cts.Cancel();
});

var value = await task.WithCancellation(cts.Token);

Console.WriteLine(value);
Console.ReadLine();



public static class Helper
{
    public static Task<TResult> WithCancellation<TResult>(this Task<TResult> task, CancellationToken ct)
    {
        var tcs = new TaskCompletionSource<TResult>();

        ct.Register(tcs.SetCanceled);

        task.ContinueWith(t => tcs.SetResult(t.Result));

        return tcs.Task;
    }
}
