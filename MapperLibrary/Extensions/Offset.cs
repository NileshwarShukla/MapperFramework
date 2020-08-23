namespace MapperLibrary
{
    using System;
    public class Offset<T> : Extensions<T>, IOperations<T>
    {
        public Offset()
        {
            Format = "DD-MM-YYYY";
        }
        public string DataType { get; set; }

        public object Value { get; set; }
        private object _value = null;
        public override object AssignValue(T o)
        {
            Type t = Type.GetType(DataType);
            object sourceVal = GetFieldValue(o); //o.GetType().GetProperty(Field).GetValue(o, null);
            if (t.Equals(typeof(DateTime)))
            {
                DateTime dt;
                if (DateTime.TryParse((string)GetSubNodeValue(o), out dt))
                {
                    _value = dt;
                }
                return Add((DateTime)sourceVal);
            }
            if (t.Equals(typeof(int)))
            {
                int numeric;
                if (Int32.TryParse((string)GetSubNodeValue(o), out numeric))
                {
                    _value = numeric;
                }
                return Add((int)sourceVal);
            }
            if (t.Equals(typeof(string)))
            {
                string val = Convert.ToString(GetSubNodeValue(o));
                if (!string.IsNullOrEmpty(val))
                {
                    _value = val;
                }
                return Add((string)sourceVal);
            }
            return null;
        }

        private DateTime Add(DateTime date)
        {
            return _value != null ? date.Add(((DateTime)_value).Subtract(date)) : date.AddDays(double.Parse((string)Value));
        }
        private int Add(int numeric)
        {
            return _value != null ? numeric + (int)_value : numeric + int.Parse((string)Value);
        }
        private string Add(string str)
        {
            return _value != null ? string.Format("{0} {1}", str, (string)_value) : string.Format("{0} {1}", str, (string)Value);
        }
    }
}
