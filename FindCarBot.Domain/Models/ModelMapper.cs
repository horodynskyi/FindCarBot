using System;
using System.ComponentModel;
using System.Text.Json.Nodes;
using FindCarBot.Domain.Services;

namespace FindCarBot.Domain.Models
{
    public static class ModelMapper {
        public static T Map<T> (JsonObject obj) where T:class, new()
        {
            T model = new();
            foreach(var destProp in model.GetType().GetProperties())
            {
                obj.TryGetPropertyValue(typeof(T).Name, out var val);
                destProp.SetValue(model, val);
            }
            
              /* foreach(var srcProp in model.GetType().GetProperties())
                {
                    if(srcProp.Name == obj.) 
                    {
                        srcProp.SetValue(model, srcProp.GetValue(obj, null));
                    }
                }*/
            

            return new T();
            /*T model = new();

            foreach(var destProp in model.GetType().GetProperties())
            {
                var srcProp = obj.GetType().GetProperty(destProp.Name);
                var propType = destProp.PropertyType;
                if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propType = new NullableConverter(propType).UnderlyingType;
                }

                if (srcProp is not null) destProp.SetValue(model, srcProp.GetValue(obj, null));
            }
            return model;*/
        }
    }
}