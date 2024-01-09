using core.Entities;
using core.Interfaces;

namespace infrastructure.Services
{
    public class ErrorService : IErrorService
    {
        private readonly IUnitOfWork unitOfWork;

        public ErrorService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreateError(ErrorEntity errorEntity)
        {
            unitOfWork.Repository<ErrorEntity>().Add(errorEntity);
            
            await unitOfWork.Complete();
        }

        public async Task UpdateError(ErrorEntity errorEntity)
        {
            unitOfWork.Repository<ErrorEntity>().Update(errorEntity);

            await unitOfWork.Complete();
        }

    }
}