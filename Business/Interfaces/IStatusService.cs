using Data.Entities;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<IResponseResult<IEnumerable<Status>>> GetAllStatusesAsync();
        Task<IResponseResult> GetStatusByNameAsync(string statusName);
        Task<IResponseResult> GetStatusByIdAsync(int id);
    }
}