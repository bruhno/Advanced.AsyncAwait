

using System.Collections.Concurrent;

var tasks = Enumerable.Range(0, 10).Select(x => Task.Run(async () =>
{
    await Task.Delay(1000);
    return x;
})).ToArray();


var arr = await Helper.WhenAllOrError(tasks);

Console.WriteLine(string.Join(",", arr.Order()));

public static class Helper
{
    public static Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
    {
        var tcs = new TaskCompletionSource<TResult[]>();

        var list = new ConcurrentBag<TResult>();

        foreach (var task in tasks)
        {
            task.ContinueWith(t =>
            {
                list.Add(t.Result);

                if (list.Count == tasks.Length)
                {
                    tcs.SetResult([.. list]);
                }
            },
            TaskContinuationOptions.OnlyOnRanToCompletion);

            task.ContinueWith(t =>
            {
                tcs.SetResult([.. list]);
            },
            TaskContinuationOptions.NotOnRanToCompletion);
        }

        return tcs.Task;
    }
}