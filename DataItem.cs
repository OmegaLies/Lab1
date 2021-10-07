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
            return $"X: {X}, Y:{Y}, Value: {C}, Abs:{C.Magnitude}";
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
                double x = rnd.Next(-500, 500) / 119;
                double y = rnd.Next(-500, 500) / 119;
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
                double sum = 0;
                foreach (DataItem i in List) { sum += i.C.Magnitude; }
                return sum / Count;
            }

        }

        public override string ToString()
        {
            return $"Type: V1DataList, Name: {base.Str},  Date: {base.Time}, Count {Count}";
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
        public int Ox { get; }
        public int Oy { get; }
        public double Dx { get; }
        public double Dy { get; }
        public System.Numerics.Complex[,] List { get; }

        public V1DataArray(string s, DateTime t) : base(s, t)
        {
            List = new System.Numerics.Complex[2, 2];
        }

        public V1DataArray(string s, DateTime t, int ox, int oy, double dx, double dy, FdblComplex F) : base(s, t)
        {
            Ox = ox;
            Oy = oy;
            Dx = dx;
            Dy = dy;
            List = new System.Numerics.Complex[Ox, Oy];
            for (int i = 0; i < Ox; ++i)
            {
                for (int j = 0; j < Oy; ++j)
                {
                    List[i, j] = F(Dx * i, Dy * j);
                }
            }
        }

        public override int Count => Ox * Oy;
        public override double AverageValue
        {
            get
            {
                double sum = 0;
                foreach (System.Numerics.Complex i in List) { sum += i.Magnitude; }
                return sum / Count;
            }
        }
        public override string ToString()
        {
            return $"Type: V1DataArray, Name: {base.Str},  Date: {base.Time}, Count: {Count}, Ox: {Ox}, Oy: {Oy}, Dx: {Dx}, Dy: {Dy}";
        }
        public override string ToLongString(string format)
        {
            string str = $"Type: V1DataArray, Name: {base.Str},  Date: {base.Time}, Count: {Count}, Ox: {Ox}, Oy: {Oy}, Dx: {Dx.ToString(format)}, Dy: {Dy.ToString(format)}\n";
            for (int i = 0; i < Ox; ++i)
            {
                for (int j = 0; j < Oy; ++j)
                {
                    str += $"X: {(Dx * i).ToString(format)}, Y: {(Dy * j).ToString(format)}, Value: {List[i, j].ToString(format)}, Abs: {List[i, j].Magnitude.ToString(format)}\n";
                }
            }
            return str;
        }
        public static implicit operator V1DataList(V1DataArray arr)
        {
            V1DataList res = new(arr.Str, arr.Time);
            for (int i = 0; i < arr.Ox; ++i)
            {
                for (int j = 0; j < arr.Oy; ++j)
                {
                    res.Add(new DataItem(arr.Dx * i, arr.Dy * j, arr.List[i, j]));
                }
            }
            return res;
        }
    }

    class V1MainCollection
    {
        private List<V1Data> List = new();
        public int Count => List.Count;
        public V1Data this[int index]
        {
            get => List[index];
        }
        public bool Contains(string ID)
        {
            if (List == null)
            {
                return false;
            }
            foreach (V1Data i in List)
            {
                if (i.Str == ID)
                {
                    return true;
                }
            }
            return false;
        }
        public bool Add(V1Data v1Data)
        {
            if (!Contains(v1Data.Str))
            {
                List.Add(v1Data);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            string str = "";
            foreach (V1Data i in List)
            {
                str += i.ToString() + " ";
            }
            return str;
        }
        public string ToLongString(string format)
        {
            string str = "";
            foreach (V1Data i in List)
            {
                str += i.ToLongString(format) + " ";
            }
            return str;
        }
    }
}
