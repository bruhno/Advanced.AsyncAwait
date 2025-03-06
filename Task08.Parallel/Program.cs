await Parallel.ForEachAsync(
    [1, 2, 3], 
    CancellationToken.None, 
    ExecuteAsync);


async ValueTask ExecuteAsync(int num, CancellationToken cancellationToken)
{
    while (true)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Console.WriteLine($"Do smth {num}");
        await Task.Delay(1000 + num * 100);
    }
}