namespace FindCarBot.Domain.Models
{
    public class PickedParameters
    {
        public long Id { get; set; }
        public BodyStyle BodyStyle { get; set; }
        public DriverType DriverType { get; set; }
        public Fuel Fuel { get; set; }
        public GearBox GearBox { get; set; }
        public Manufacture Manufacture { get; set; }
        public Dates Dates { get; set; }
        public PriceRange PriceRange { get; set; }

 
        public bool FieldsIsNull()
        {
            if (BodyStyle == null)
                return true;
            if (DriverType == null)
                return true;
            if (Fuel == null)
                return true;
            if (GearBox == null)
                return true;
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
                    str = "Choose type of fuel:";
                    break;
                case Models.Manufacture:
                    str = "Choose country of manufacture:";
                    break;
                case Models.DriverType:
                    str = "Choose type of driver:";
                    break;
                case Models.BodyStyle:
                    str = "Choose type of body:";
                    break;
                case Models.GearBox:
                    str = "Choose type of gearbox:";
                    break;
                case Models.Dates:
                    str = "Type range of years format is (yyyy-yyyy):";
                    break;
                case Models.PriceRange:
                    str = "Type range of price format is (num-num):";
                    break;
                default: return null;
                    
            }
            return str;
        }
        
        public void SetField(BaseModel? baseModel)
        {
            switch (baseModel)
            {
                case GearBox box:
                    GearBox = box;
                    break;
                case DriverType type:
                    DriverType = type;
                    break;
                case Fuel fuel:
                    Fuel = fuel;
                    break;
                case BodyStyle style:
                    BodyStyle = style;
                    break;
                case Manufacture model:
                    Manufacture = model;
                    break;
                case Dates dates:
                    Dates = dates;
                    break;
                case PriceRange range:
                    PriceRange = range;
                    break;
            }
        }
    }
}