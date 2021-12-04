using System;
using FindCarBot.Domain.Services;

namespace FindCarBot.Domain.Models
{
    public static class ModelMapper {
        public static T Map<T> (object obj) where T:class, new() 
        {
            T model = new();

            foreach(var destProp in model.GetType().GetFields())
            {
                foreach(var srcProp in obj.GetType().GetProperties())
                {
                    if(destProp.Name == srcProp.Name)
                    {
                        /*var val = destProp.PropertyType; srcProp.GetValue(obj, null);*/
                        destProp.SetValue(model,   srcProp.GetValue(obj, null));
                    }
                }
            }
            return model;
        }
    }
}