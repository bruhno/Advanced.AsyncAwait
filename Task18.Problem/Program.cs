var tcs = new TaskCompletionSource();

var cacheService = Task.Run(() =>
{
    Console.WriteLine("C0 " + Environment.CurrentManagedThreadId);
    Thread.Sleep(1000);
    tcs.SetResult();
    while (true)
    {
        Console.WriteLine("C1 " + Environment.CurrentManagedThreadId);
        Thread.Sleep(1000);
    }
});

var processingService = Task.Run(async () =>
{
    Console.WriteLine("P0 " + Environment.CurrentManagedThreadId);
    await tcs.Task;

    while (true)
    {
        Console.WriteLine("P1 " + Environment.CurrentManagedThreadId);
        Thread.Sleep(1000);
    }
});

await Task.WhenAll(cacheService, processingService);

Console.WriteLine("M1 "+Environment.CurrentManagedThreadId);