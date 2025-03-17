using System;

delegate int MyFunc();

class Program
{
    public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs e);

    public class ValueEventArgsAsync : EventArgs
    {
    }


    public static event AsyncEventHandler<ValueEventArgsAsync> OnProcess = default!;

    static async Task Main(string[] args)
    {
        OnProcess += async (e, a) =>
        {
            await Task.Delay(5000);
            Console.WriteLine("1");
        };

        OnProcess += async (e, a) =>
        {
            await Task.Delay(200);
            Console.WriteLine("2");
        };

        OnProcess += async (e, a) =>
        {
            await Task.Delay(300);
            Console.WriteLine("3");
        };


        await Task.WhenAll(OnProcess.GetInvocationList()
            .Select(d => ((AsyncEventHandler<ValueEventArgsAsync>)d)(null!, null!)));

        await Task.WhenAll(OnProcess.GetInvocationList()
            .Select(d => ((AsyncEventHandler<ValueEventArgsAsync>)d)(null!, null!)));

        Console.WriteLine("Finish");
    }

    //private static Task InvokeOnProcess(object sender, ValueEventArgsAsync args)
    //{
    //    Task task = null!;

    //    foreach (var h in OnProcess.GetInvocationList())
    //    {
    //        var handler = (AsyncEventHandler<ValueEventArgsAsync>)h;

    //        task = (task is null)
    //            ? Task.Run(() => handler(sender, args))
    //            : task.ContinueWith(_ => handler(sender, args)).Unwrap();
    //    }

    //    return task;
    //}
}