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
            await Task.Delay(100);
            Console.WriteLine("1");
        };

        OnProcess += async (e, a) =>
        {
            await Task.Delay(10000);
            Console.WriteLine("2");
        };


        await OnProcess.Invoke(null!, null!);
        await OnProcess.Invoke(null!, null!);



        Console.WriteLine("Finish");
    }
}