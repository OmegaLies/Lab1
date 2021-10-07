using System;
using System.Numerics;

namespace Lab1
{
    class Program
    {
        static Complex Func(double x, double y) => new Complex((x + y) * (x + y) + 1, (x - y) * (x - y) - 1);
        static void Main()
        {
            string format = "F3";
            V1DataArray Array = new("Array one", DateTime.Now, 3, 4, 0.7, 0.4, Func);
            Console.WriteLine(Array.ToLongString(format));
            V1DataList List = Array;
            Console.WriteLine(List.ToLongString(format));
            Console.WriteLine($"{Array.GetType()}: Count - {Array.Count.ToString(format)}, " +
                $"Average Value - {Array.AverageValue.ToString(format)}");
            Console.WriteLine($"{List.GetType()}: Count - {List.Count.ToString(format)}, " +
                $"Average Value - {List.AverageValue.ToString(format)}\n");

            V1DataArray Array_2 = new("Array two", DateTime.Now, 2, 2, 0.8, 1.1, Func);
            V1DataList List_1 = new("List one", DateTime.Now);
            List_1.AddDefaults(6, Func);
            V1DataList List_2 = new("List two", DateTime.Now);
            List_2.AddDefaults(4, Func);
            V1MainCollection Collection = new();
            Collection.Add(Array);
            Collection.Add(Array_2);
            Collection.Add(List_1);
            Collection.Add(List_2);
            Console.WriteLine(Collection.ToLongString(format));

            for (int i = 0; i < Collection.Count; ++i)
            {
                Console.WriteLine($"{Collection[i].GetType()}: Count - {Collection[i].Count.ToString(format)}, " +
                    $"Average Value - {Collection[i].AverageValue}");
            }
        }
    }
}
