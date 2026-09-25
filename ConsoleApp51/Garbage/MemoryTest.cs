using ConsoleApp51.Models;

namespace ConsoleApp51.Tests;

internal class MemoryTest
{
    public void Run()
    {
        long before =
            GC.GetTotalMemory(false);

        long allocatedBefore =
            GC.GetAllocatedBytesForCurrentThread();

        List<OrderItem> items = new();

        for (int i = 0; i < 100000; i++)
        {
            Product product =
                new Product(
                    i,
                    "Temporary",
                    "Test",
                    10,
                    1,
                    Enum.Category.Electronics);

            items.Add(
                new OrderItem(product, 1));
        }

        Console.WriteLine(
            $"Before: {before:N0}");

        Console.WriteLine(
            $"Allocated Before: " +
            $"{allocatedBefore:N0}");

        Console.WriteLine(
            $"Generation: " +
            $"{GC.GetGeneration(items)}");

        items = null!;

        GC.Collect();

        GC.WaitForPendingFinalizers();

        long after =
            GC.GetTotalMemory(true);

        long allocatedAfter =
            GC.GetAllocatedBytesForCurrentThread();

        Console.WriteLine(
            $"After GC: {after:N0}");

        Console.WriteLine(
            $"Allocated After: " +
            $"{allocatedAfter:N0}");
    }
}
