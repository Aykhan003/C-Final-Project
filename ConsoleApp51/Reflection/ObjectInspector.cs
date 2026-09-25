namespace ConsoleApp51.Reflection;

internal class ObjectInspector
{
    public static void InspectObject(object obj)
    {
        Type type = obj.GetType();
        Console.WriteLine($"Type: {type.FullName}");
        Console.WriteLine("Properties:");
        foreach (var property in type.GetProperties())
        {
            Console.WriteLine($"- {property.Name} ({property.PropertyType.Name})");
        }
        Console.WriteLine("Fields:");
        foreach (var field in type.GetFields())
        {
            Console.WriteLine($"- {field.Name} ({field.FieldType.Name})");
        }
        foreach (var method in type.GetMethods())
        {
            Console.WriteLine($"- {method.Name} ({method.ReturnType.Name})");
        }
    }
}
