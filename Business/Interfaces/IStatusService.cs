using System.Linq.Expressions;
using Data.Entities;
using Domain.Models;

namespace Business.Interfaces
{
    public interface IStatusService
    {
        Task<IResponseResult<IEnumerable<Status>>> GetAllStatusesAsync();
        Task<IResponseResult> GetStatusByExpressionAsync(Expression<Func<StatusEntity, bool>> expression);
    }
}