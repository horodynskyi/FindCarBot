using System.Collections.Generic;
using System.Threading.Tasks;
using FindCarBot.Domain.Models;

namespace FindCarBot.Domain.Abstractions
{
    public interface IAutoRiaService
    {
        Task<List<Mark>> GetMarks();
        Task<List<BodyStyle>> GetBodyStyles();
        Task<List<Fuel>> GetFuelTypes();
        
        Task<List<GearBox>> GetGearBoxes();
        Task<List<DriverType>> GetDriverTypes();
    }
}