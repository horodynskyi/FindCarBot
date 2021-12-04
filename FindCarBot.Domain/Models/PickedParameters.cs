using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FindCarBot.Domain.Models
{
    public class PickedParameters
    {
        public long Id { get; set; }
        private Mark Mark { get; set; }
        private BodyStyle BodyStyle { get; set; }
        private DriverType DriverType { get; set; }
        private Fuel Fuel { get; set; }
        private GearBox GearBox { get; set; }
        private ModelAuto ModelAuto { get; set; }
        private Manufacture Manufacture { get; set; }

        public Mark GetMark()
        {
            return Mark;
        }
        public bool FieldsIsNull()
        {
            if (Mark == null)
                return true;
            if (BodyStyle == null)
                return true;
            if (DriverType == null)
                return true;
            if (Fuel == null)
                return true;
            if (GearBox == null)
                return true;
            if (ModelAuto == null)
                return true;
            if (Manufacture == null)
                return true;
            return false;
        }

        public override string ToString()
        {
            return $"Паливо:{Fuel.Name} коробка{GearBox.Name}";
        }

        public BaseModel Next()
        {
            if (Fuel == null)
                return new Fuel();
            if (Mark == null)
                return new Mark();
            if (GearBox == null)
                return new GearBox();
            if (BodyStyle == null)
                return new BodyStyle();
            if (DriverType == null)
                return new DriverType();
            if (Manufacture == null)
                return new Manufacture();
            if (ModelAuto == null || Mark!=null)
                return new ModelAuto();
            return null;
        }

        public string GetMessageFromField(BaseModel model)
        {
            string str ="";
            switch (model)
            {
                case Models.Fuel:
                    str = $"Choose type of fuel:";
                    break;
                case Models.Manufacture:
                    str = $"Choose country of manufacture:";
                    break;
                case Models.Mark:
                    str = $"Type here to search mark of auto:";
                    break;
                case Models.DriverType:
                    str = $"Choose type of driver:";
                    break;
                case Models.BodyStyle:
                    str = $"Choose type of body:";
                    break;
                case Models.GearBox:
                    str = $"Choose type of gearbox:";
                    break;
                case Models.ModelAuto:
                    str = $"Type here to search model of auto:";
                    break;
                default: return null;
                    
            }
            return str;
        }


        public void SetField(string str, BaseModel baseModel)
        {
            if (str == "Mark")
                Mark = (Mark) baseModel;
            if (str == "BodyStyle")
                BodyStyle = (BodyStyle) baseModel;
            if (str == "DriverType")
                DriverType = (DriverType) baseModel;
            if (str == "Fuel")
                Fuel = (Fuel) baseModel;
            if (str == "GearBox")
                GearBox = (GearBox) baseModel;
            if (str == "ModelAuto")
                ModelAuto = (ModelAuto) baseModel;
            if (str == "Manufacture")
                Manufacture = (Manufacture) baseModel;
        }
    }
}