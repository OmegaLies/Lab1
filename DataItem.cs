using System;
using System.Collections.Generic;

namespace Lab1
{
    struct DataItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public System.Numerics.Complex C { get; set; }

        public DataItem(double a, double b, System.Numerics.Complex i)
        {
            X = a;
            Y = b;
            C = i;
        }

        public string ToLongString(string format)
        {
            return $"X: {X.ToString(format)}, Y:{Y.ToString(format)}, Value: {C.ToString(format)}, Abs: {C.Magnitude.ToString(format)}";
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
    public delegate System.Numerics.Complex FdblComplex(double x, double y);

    abstract class V1Data
    {
        public string Str { get; }
        public DateTime Time { get; }

        public V1Data(string s, DateTime t)
        {
            Str = s;
            Time = t;
        }
        public abstract int Count { get; }
        public abstract double AverageValue { get; }
        public abstract string ToLongString(string format);
        public override string ToString()
        {
            return base.ToString();
        }
    }

    class V1DataList : V1Data
    {
        public List<DataItem> List { get; }

        public V1DataList(string s, DateTime t) : base(s, t)
        {
            List = new();
        }

        public bool Add(DataItem newItem)
        {
            foreach (DataItem i in List)
            {
                if (newItem.X == i.X && newItem.Y == i.Y)
                {
                    return false;
                }
            }
            List.Add(newItem);
            return true;
        }

        public int AddDefaults(int nItems, FdblComplex F)
        {
            Random rnd = new();
            int count = 0;
            for (int i = 0; i < nItems; ++i)
            {
                int x = rnd.Next(-1000, 1000);
                int y = rnd.Next(-1000, 1000);
                DataItem tmp = new(x, y, F(x, y));
                if (Add(tmp))
                {
                    ++count;
                }
            }
            return count;
        }

        public override int Count => List.Count;

        public override double AverageValue
        {
            get
            {
                System.Numerics.Complex sum = 0;
                foreach (DataItem i in List) { sum += i.C; }
                return 0;
            }

        }

        public override string ToString()
        {
            return $"Type: V1DataList, Name: {base.Str},  Date: {base.Time}, Count {Count} ";
        }

        public override string ToLongString(string format)
        {
            string str = $"Type: V1DataList, Name: {base.Str},  Date: {base.Time}, Count: {Count}\n";
            foreach (DataItem i in List)
            {
                str += i.ToLongString(format) + "\n";
            }
            return str;
        }
    }

    class V1DataArray : V1Data
    {

    }

    class V1MainCollection
    {
        private List<V1Data> List;
        public int Count => List.Count;
        public V1Data this[int index]
        {
            get => List[index];
        }
    }
}
