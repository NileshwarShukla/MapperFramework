namespace MapperLibrary
{
    using System;
    public class Refer<T> : Extensions<T>, IOperations<T>
    {
        public Refer()
        {
            Format = "DD-MM-YYYY";
        }
        public string DataType { get; set; }

        private object _value = null;
        public override object AssignValue(T o)
        {
            Type t = Type.GetType(DataType);
            object sourceVal = GetFieldValue(o);
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

        private object Add(DateTime dateTime)
        {
            return _value != null ? string.Format("{0} {1}", Convert.ToString(_value), dateTime.ToString(Format)) : dateTime.ToString(Format);
        }
        private object Add(int numeric)
        {
            return _value != null ? (int)_value + numeric : numeric;
        }
        private object Add(string str)
        {
            return _value != null ? string.Format("{0} {1}", str, (string)_value) : str;
        }
    }
}
