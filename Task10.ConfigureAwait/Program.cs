var my = new MySyncronizationContext();

SynchronizationContext.SetSynchronizationContext(my);

Console.WriteLine($"Begin: {Environment.CurrentManagedThreadId}: SyncCtx: {SynchronizationContext.Current}");

await DoDelay(1000);


Console.WriteLine($"End: {Environment.CurrentManagedThreadId}: SyncCtx: {SynchronizationContext.Current}");

async Task DoDelay(int millisec)
{
    Console.WriteLine($"DoDelay1: SyncCtx: {SynchronizationContext.Current}");
    await Task.Delay(millisec).ConfigureAwait(false);
}

class MySyncronizationContext : SynchronizationContext
{
    public override void Post(SendOrPostCallback d, object? state)
    {
        Console.WriteLine($"Post: {Environment.CurrentManagedThreadId}: SyncCtx: {Current}");
        base.Post(s =>
        {
            SetSynchronizationContext(this);
            d(s);
        }, state);
    }
}

