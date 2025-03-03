//How to replace lines 10 and 11 so that this code runs concurrently

Func<int, Task> process = async x =>
{
    await Task.Delay(1000); //Stub for some async operation.
    Console.WriteLine($"End processing {x}");
};

int[] array = [1, 2, 3, 4, 5];
var tasks = array.Select(a => Task.Run(() => process(a)));
await Task.WhenAll(tasks);
//foreach (int i in array)
//    await process(i);

Console.WriteLine("All items processed");
