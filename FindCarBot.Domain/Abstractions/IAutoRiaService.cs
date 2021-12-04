using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FindCarBot.Domain.Models;

namespace FindCarBot.Domain.Abstractions
{
    public interface IAutoRiaService
    {
        Task<IEnumerable<Mark>> GetMarks();
        Task<IEnumerable<BodyStyle>> GetBodyStyles();
        Task<IEnumerable<Fuel>> GetFuelTypes();
        Task<IEnumerable<GearBox>> GetGearBoxes();
        Task<IEnumerable<DriverType>> GetDriverTypes();
        Task<IEnumerable<Manufacture>> GetManufacture();
        Task<IEnumerable<ModelAuto>> GetModelAuto(int value);
        Task<IEnumerable<BaseModel>> GetParameters<T>(T entity);

    }
}