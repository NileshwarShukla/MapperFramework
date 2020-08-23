
namespace MapperLibrary
{
    using System;
    public class Basic<T>:Extensions<T>, IOperations<T>
    {
        public Basic()
        {
            Format = "DD-MM-YYYY";
        }
        public object Value { get; set; }
        private object _value;
        public override object AssignValue(T o)
        {
            _value=GetSubNodeValue(o);
            if(_value==null)
            {
                return Convert.ToString(Value);
            }
            if (_value.GetType()==(typeof(DateTime)))
            {
                return ((DateTime)Value).Add(((DateTime)_value).Subtract(((DateTime)Value)));
            }
            if (_value.GetType() == (typeof(int)))
            {
                return (int)Value+(int)_value;
            }
            if (_value.GetType() == (typeof(string)))
            {
                return string.Format("{0} {1}", Value, Convert.ToString(_value));
            }
            return Value;
        }
    }
}

