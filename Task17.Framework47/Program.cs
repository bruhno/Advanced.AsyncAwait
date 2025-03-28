using System.Diagnostics;

class Program
{
    private static TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

    static void Main(string[] args)
    {
        var sw = Stopwatch.StartNew();

        _ = Task.Run(async () =>
        {
            Console.WriteLine(sw.ElapsedMilliseconds+ $" A0 {Thread.CurrentThread.ManagedThreadId}");
            await tcs.Task;
            Thread.Sleep(1000);
            Console.WriteLine(sw.ElapsedMilliseconds + $" A {Thread.CurrentThread.ManagedThreadId}");
        });

        _ = Task.Run(async () =>
        {
            Console.WriteLine(sw.ElapsedMilliseconds + $" B0 {Thread.CurrentThread.ManagedThreadId}");

            await tcs.Task;

            Thread.Sleep(500);
            Console.WriteLine(sw.ElapsedMilliseconds + $" B {Thread.CurrentThread.ManagedThreadId}");
        });

        Thread.Sleep(1000);
        tcs.SetResult(null!);
        Console.WriteLine(sw.ElapsedMilliseconds + $" MM {Thread.CurrentThread.ManagedThreadId}");

        Console.ReadKey();
    }
}