namespace MapperLibrary
{
    using System;
    public abstract class Extensions<T>
    {
        private string defaultValue = "";
        public string Format { get; set; }       
        public string Field { get; set; }
        public Refer<T> Refrence { get; set; }
        public Offset<T> Offset { get; set; }
        public Hook<T> Hook { get; set; }
        public Basic<T> Basic { get; set; }
        public abstract object AssignValue(T o);
        public void AssignValue(T item, Operation? operation)
        {
            IOperations<T> oper;
            switch (operation)
            {
                case Operation.Refer:
                    oper = Refrence;
                    defaultValue = Convert.ToString(oper.AssignValue(item));
                    break;
                case Operation.Offset:
                    oper = Offset;
                    defaultValue = Convert.ToString(oper.AssignValue(item));
                    break;
                case Operation.Hook:
                    oper = Hook;
                    defaultValue = Convert.ToString(oper.AssignValue(item));
                    break;
                case Operation.Basic:
                    oper = Basic;
                    defaultValue = Convert.ToString(oper.AssignValue(item));
                    break;
                default:
                    defaultValue = null;
                    break;
            }
        }
        protected object GetSubNodeValue(T o)
        {
            if (this.Refrence != null)
            {
                return this.Refrence.AssignValue(o);
            }
            if (this.Offset != null)
            {
                return this.Offset.AssignValue(o);
            }
            if (this.Hook != null)
            {
                return this.Hook.AssignValue(o);
            }
            if (this.Basic != null)
            {
                return this.Basic.AssignValue(o);
            }
            return null;
        }
        protected object GetFieldValue(T o)
        {
            if (Field.Contains("."))
            {               
                object val = GetPropertyValue(o, Field);
                return val;
            }
            return o.GetType().GetProperty(Field).GetValue(o, null);
        }
        private object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }
        public string GetValue()
        {
            return defaultValue;
        }

    }
}
