using System.Collections.Generic;
using System.Threading.Tasks;
using FindCarBot.Domain.Models;

namespace FindCarBot.Domain.Abstractions
{
    public interface IAutoRiaService
    {
        Task GetAllAutoAttributes(string token);
        Task<List<Mark>> GetTypesOfAuto();
    }
}