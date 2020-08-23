using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MapperLibrary
{
    public class TargetConversion
    {
        public string PropName { get; set; }
        public object Value { get; set; }
        public PropertyInfo PropInfo { get; set; }
    }
    public class Mapper<T>
    {
        private CustomActions<T> customActions;
        private Extensions<T> mapper;
        public Mapper(CustomActions<T> actions)
        {
           customActions = actions;
            mapper = new Default<T>();
        }

        public object CreateMapping( T source, object target,string json)
        {
            Dictionary<string, TargetConversion> properties = target.GetType().GetProperties().Select(a=>new TargetConversion { PropName=a.Name,PropInfo=a,Value=null }).ToDictionary(a=>a.PropName);
            var b = JObject.Parse(json);
            Dictionary<string, TargetConversion> dict = new Dictionary<string, TargetConversion>();

            foreach (JToken k in b.SelectToken("mappings").Children())
            {                  
                SetOperations(mapper,source,k);
                string propName=  k["targetFieldName"].ToString();
                TargetConversion val = properties[propName];
                val.Value = mapper.GetValue();
                dict.Add(propName, val);
            }
            foreach (KeyValuePair<string, TargetConversion> obj in dict)
            {              
                if (obj.Value.PropInfo != null)
                {
                    AssignValueToTarget(obj,target);                   
                }
            }
            return target;
        }

        private void AssignValueToTarget(KeyValuePair<string, TargetConversion> obj,object target)
        {
            if (obj.Value.PropInfo.PropertyType == typeof(DateTime))
            {

                obj.Value.PropInfo.SetValue(target, DateTime.Parse((string)obj.Value.Value));
            }
            else if (obj.Value.PropInfo.PropertyType == typeof(int))
            {
                obj.Value.PropInfo.SetValue(target, int.Parse((string)obj.Value.Value));
            }
            else
            {
                obj.Value.PropInfo.SetValue(target, obj.Value.Value);
            }
        }

        private void SetOperations(Extensions<T> mapper, T source, JToken k)
        {
            JToken refer = k.SelectToken("ref");
            if (refer != null)
            {               
                mapper.Refrence = new Refer<T>();
                mapper.Refrence.DataType = Convert.ToString(k["ref"]["type"]);
                mapper.Refrence.Field = Convert.ToString(k["sourceFieldName"]);
                SetOperations(mapper.Refrence, source, k["ref"]);
                mapper.AssignValue(source, Operation.Refer);
            }

            JToken offset = k.SelectToken("offset");
            if (offset != null)
            {
                mapper.Offset = new Offset<T>();
                mapper.Offset.DataType = Convert.ToString(k["offset"]["type"]);
                mapper.Offset.Field = Convert.ToString(k["sourceFieldName"]);
                mapper.Offset.Value = Convert.ToString(k["offset"]["value"]);
                SetOperations(mapper.Offset, source, k["offset"]);
                mapper.AssignValue(source, Operation.Offset);
            }
            JToken hook = k.SelectToken("hook");
            if (hook != null)
            {
                mapper.Hook = new Hook<T>(customActions);
                mapper.Hook.Add(Convert.ToString(k["hook"]["methodName"]), source);
                SetOperations(mapper.Hook, source, k["hook"]);
                mapper.AssignValue(source, Operation.Hook);
                mapper.Hook.Remove();
            }
            JToken basic = k.SelectToken("basic");
            if (basic!=null)
            {
                mapper.Basic = new Basic<T>();
                mapper.Basic.Value = Convert.ToString(k["basic"]["value"]);
                SetOperations(mapper.Basic, source, k["basic"]);
                mapper.AssignValue(source, Operation.Basic);
            }

        }
    }
}
