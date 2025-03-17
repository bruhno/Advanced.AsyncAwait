
var task1 = Task.Run(async () =>
    {
        await Task.Delay(1000);
        return 1;
    });

var task2 = Task.Run(async () =>
{
    await Task.Delay(2000);
    return 2;
});

var task3 = Task.Run(async () =>
{
    await Task.Delay(3000);
    throw new OperationCanceledException();
    return 3;
});

var task4 = Task.Run(async () =>
{
    await Task.Delay(4000);
    return 4;
});

var arr = await Helper.WhenAllOrError(task1, task2, task3, task4);

Console.WriteLine(string.Join(",", arr));

public static class Helper
{
    public static Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
    {
        var tcs = new TaskCompletionSource<TResult[]>();

        var list = new List<TResult>();

        foreach (var task in tasks)
        {
            task.ContinueWith(t =>
            {
                list.Add(t.Result);

                if (list.Count == tasks.Length)
                {
                    tcs.SetResult(list.ToArray());
                }
            },
            TaskContinuationOptions.OnlyOnRanToCompletion);

            task.ContinueWith(t =>
            {
                Console.WriteLine(t.Status);
                tcs.SetResult(list.ToArray());
            },
            TaskContinuationOptions.NotOnRanToCompletion);
        }

        return tcs.Task;
    }
}