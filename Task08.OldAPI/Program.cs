var cts = new CancellationTokenSource();

var t = Task.Factory.StartNew(() => ExecuteAsync(cts.Token), TaskCreationOptions.LongRunning);

var ss = Task.Run(() => 1);

await t.Unwrap();

async Task ExecuteAsync(CancellationToken ct)
{
    while (!ct.IsCancellationRequested)
    {
        Console.WriteLine($"Do smth");
        await Task.Delay(1000);
    }
}