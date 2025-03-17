using System.ComponentModel.DataAnnotations;

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

        var mre = new ManualResetEvent(false);

        _ = Task.Run(() =>
        {
            WaitHandle.WaitAny([
                ct.WaitHandle,
                mre
            ]);

            if (!tcs.Task.IsCompleted)
            {
                tcs.SetCanceled();
            }
        });

        task.ContinueWith(t =>
        {
            if (!tcs.Task.IsCompleted)
            {
                mre.Set();
                tcs.SetResult(t.Result);
            }
        });

        return tcs.Task.ContinueWith(t =>
        {
            mre.Dispose();
            return t.Result;
        });
    }
}
