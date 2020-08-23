using System;
using System.CodeDom;
using System.Reflection;

namespace MapperLibrary
{
    public class Hook<T> : Extensions<T>, IHooks<T>, IOperations<T>
    {
 
        private object _value = null;
        public Hook(CustomActions<T> customActions)
        {
            CustomHooks = customActions;
            Format = "DD-MM-YYYY";
        }
        public CustomActions<T> CustomHooks { get; set; }
        Method<T> method;

        public override object AssignValue(T o)
        {
             _value=  GetSubNodeValue(o);
            object val=method.Invoke(o);
            if (_value == null)
            {
                return val;
            }
            if(_value.GetType()==typeof(string))
            {
                return string.Format("{0} {1}",Convert.ToString(val), Convert.ToString(_value));
            }
            if (_value.GetType() == typeof(int))
            {
                return  (int)method.Invoke(o)+(int)_value;
            }
            if (_value.GetType() == typeof(DateTime))
            {
                return _value != null ? ((DateTime)val).Add(((DateTime)_value).Subtract(((DateTime)val))) : ((DateTime)val);
            }
            return null;
        }
        public void Add(Method<T> delegat)
        {
            method = delegat;
        }
        public void Add(string methodName, T o)
        {
                MethodInfo myInfo = this.CustomHooks.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            //this.CustomHooks.GetType().GetMethods().FirstOrDefault(a=>a.Name==methodName);
            Type method = typeof(Method<T>);
                Add((Method<T>)Delegate.CreateDelegate(method, this.CustomHooks, myInfo, true));
        }
        public void Remove()
        {
            method = null;
        }

    }
}
