using Domain.Models;

namespace Business.Interfaces;

public interface IRoleService
{
    Task<IResponseResult<IEnumerable<Role>>> GetAllRolessAsync();
}