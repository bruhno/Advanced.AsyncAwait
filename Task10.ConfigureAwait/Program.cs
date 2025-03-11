var my = new MySyncronizationContext();

SynchronizationContext.SetSynchronizationContext(my);

Console.WriteLine($"Begin: {Environment.CurrentManagedThreadId}: SyncCtx: {SynchronizationContext.Current}");

var t = DoDelay(100);

Thread.Sleep(200);

await t;//.ConfigureAwait(ConfigureAwaitOptions.ForceYielding); ;


Console.WriteLine($"End: {Environment.CurrentManagedThreadId}: SyncCtx: {SynchronizationContext.Current}");

async Task DoDelay(int millisec)
{
    Console.WriteLine($"DoDelay1: {Environment.CurrentManagedThreadId}: SyncCtx: {SynchronizationContext.Current}");

    await Task.Delay(millisec);//.ConfigureAwait(ConfigureAwaitOptions.ForceYielding);    

    Console.WriteLine($"DoDelay2: {Environment.CurrentManagedThreadId}: SyncCtx: {SynchronizationContext.Current}");

    //throw new InvalidOperationException("ABCD");
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

