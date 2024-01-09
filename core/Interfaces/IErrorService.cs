using core.Entities;
using core.Specifications;

namespace core.Interfaces
{
    public interface IErrorService
    {
        Task CreateError(ErrorEntity errorEntity);

        Task UpdateError(ErrorEntity errorEntity);
        
    }
}
