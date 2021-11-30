namespace FindCarBot.Domain.Models
{
    public class PickedParameters
    {
        public long Id { get; set; }
        public Mark Mark { get; set; }
        public BodyStyle BodyStyle { get; set; }
        public DriverType DriverType { get; set; }
        public Fuel Fuel { get; set; }
        public GearBox GearBox { get; set; }
        public ModelAuto ModelAuto { get; set; }
    }
}