using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FindCarBot.Domain.Models
{
    public class PickedParameters
    {
        public long Id { get; set; }
      //  public Mark Mark { get; set; }
        public BodyStyle BodyStyle { get; set; }
        public DriverType DriverType { get; set; }
        public Fuel Fuel { get; set; }
        public GearBox GearBox { get; set; }
       // public ModelAuto ModelAuto { get; set; }
        public Manufacture Manufacture { get; set; }
        public Dates Dates { get; set; }
        public PriceRange PriceRange { get; set; }

 
        public bool FieldsIsNull()
        {
            /*if (Mark == null)
                return true;*/
            if (BodyStyle == null)
                return true;
            if (DriverType == null)
                return true;
            if (Fuel == null)
                return true;
            if (GearBox == null)
                return true;
            /*if (ModelAuto == null)
                return true;*/
            if (Manufacture == null)
                return true;
            if (Dates == null)
                return true;
            if (PriceRange == null)
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
            /*if (Mark == null)
                return new Mark();*/
            if (GearBox == null)
                return new GearBox();
            if (BodyStyle == null)
                return new BodyStyle();
            if (DriverType == null)
                return new DriverType();
            if (Manufacture == null)
                return new Manufacture(); 
            if (Dates == null)
                return new Dates();
            /*if (ModelAuto == null && Mark!=null)
                return new ModelAuto();*/
            if (PriceRange == null)
                return new PriceRange();
            
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
                /*case Models.Mark:
                    str = $"Type here to search mark of auto:";
                    break;*/
                case Models.DriverType:
                    str = $"Choose type of driver:";
                    break;
                case Models.BodyStyle:
                    str = $"Choose type of body:";
                    break;
                case Models.GearBox:
                    str = $"Choose type of gearbox:";
                    break;
                case Models.Dates:
                    str = $"Choose year of manufacture:";
                    break;
                
                /*case Models.ModelAuto:
                    str = $"Type here to search model of auto:";
                    break;*/
                case Models.PriceRange:
                    str = $"Type price range format is (num-num):";
                    break;
                default: return null;
                    
            }
            return str;
        }


        public void SetField(BaseModel baseModel)
        {
            switch (baseModel)
            {
                case Models.GearBox box:
                    GearBox = box;
                    break;
                case Models.DriverType type:
                    DriverType = type;
                    break;
                case Models.Fuel fuel:
                    Fuel = fuel;
                    break;
                case Models.BodyStyle style:
                    BodyStyle = style;
                    break;
                /*case Models.Mark mark:
                    Mark = mark;
                    break;*/
                case Models.Manufacture model:
                    Manufacture = model;
                    break;
                case Models.Dates dates:
                    Dates = dates;
                    break;
                case Models.PriceRange range:
                    PriceRange = range;
                    break;
            }
            /*if (baseModel is Mark)
            {
                
            }
            if (str == Mark.Name)
                Mark = (Mark) baseModel;
            if (str == BodyStyle.Name)
                BodyStyle = (BodyStyle) baseModel;
            if (str == DriverType.Name)
                DriverType = (DriverType) baseModel;
            if (str == Fuel.Name)
                Fuel = (Fuel) baseModel;
            if (str == GearBox.Name)
                GearBox = (GearBox) baseModel;
            if (str == ModelAuto.Name)
                ModelAuto = (ModelAuto) baseModel;
            if (str == Dates.Name)
                Dates = (Dates) baseModel;
            if (str == nameof(Manufacture))
                Manufacture = (Manufacture) baseModel;
            if (str == nameof(PriceRange))
            {
                
            }*/
        }
    }
}