var t = CreateTask2();

Thread.Sleep(1000);

Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
await t;
Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

Task CreateTask1()
{
    Thread.Sleep(1000);
    return Task.CompletedTask;
}

async Task CreateTask2()
{
    await Task.Delay(500);
}
