var t = CreateTask3();

Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
await t;
Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

Task CreateTask1()
{
    Thread.Sleep(500);
    return Task.CompletedTask;
}

async Task CreateTask2()
{
    await Task.Delay(500);
}

async Task CreateTask3()
{
    Thread.Sleep(1000);
}
