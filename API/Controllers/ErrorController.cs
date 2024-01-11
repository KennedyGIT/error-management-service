using API.Dtos;
using API.Errors;
using API.Helpers;
using core.Entities;
using core.Interfaces;
using core.Specifications;
using API.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorization.Authorize]
    public class ErrorController : BaseApiController
    {
        private readonly IErrorService errorService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository<ErrorEntity> errorRepo;

        public ErrorController(IErrorService errorService, 
            IHttpContextAccessor httpContextAccessor, 
            IUnitOfWork unitOfWork, IGenericRepository<ErrorEntity> errorRepo)
        {
            this.errorService = errorService;
            this.httpContextAccessor = httpContextAccessor;
            this.unitOfWork = unitOfWork;
            this.errorRepo = errorRepo;
        }

        [HttpPost]
        public async Task<ActionResult> CreateError(ErrorDto errorDto) 
        {
            if (errorDto == null) return BadRequest(new ApiResponse(400));

            var spec = new ErrorCodeSpecification(errorDto.ErrorCode);

            var totalItems = await errorRepo.CountAsync(spec);

            if(totalItems > 0) { return BadRequest(new ApiResponse(400, "Error code already exists")); }

            var user = (UserEntity)httpContextAccessor.HttpContext.Items["User"];

            var errorEntity = new ErrorEntity()
            {
                ErrorTitle = errorDto.ErrorTitle,
                ErrorCode = errorDto.ErrorCode,
                ErrorDescription = errorDto.ErrorDescription,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = user.Username,
                Version = 1,
                UpdatedAt= DateTime.UtcNow,
                UpdatedBy = user.Username,

            };

            await errorService.CreateError(errorEntity);

            return Ok(new ApiResponse(201, "Error created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateError(int id, ErrorDto errorDto) 
        {
            var existingError = await unitOfWork.Repository<ErrorEntity>().GetByIdAsync(id);

            if (existingError == null) return NotFound(new ApiResponse(404));

            var spec = new ErrorCodeSpecification(errorDto.ErrorCode);

            var totalItems = await errorRepo.CountAsync(spec);

            if (existingError.ErrorCode != errorDto.ErrorCode)
            {
                if (totalItems > 0) { return BadRequest(new ApiResponse(400, "Error code already exists")); }
            }

            var user = (UserEntity)httpContextAccessor.HttpContext.Items["User"];

            existingError.Version = existingError.Version + 1;

            existingError.UpdatedAt = DateTime.UtcNow;

            existingError.UpdatedBy = user.Username;

            existingError.ErrorCode = !string.IsNullOrEmpty(errorDto.ErrorCode) ? errorDto.ErrorCode : existingError.ErrorCode;

            existingError.ErrorDescription = !string.IsNullOrEmpty(errorDto.ErrorDescription) ? errorDto.ErrorDescription : existingError.ErrorDescription;

            existingError.ErrorTitle = !string.IsNullOrEmpty(errorDto.ErrorTitle) ? errorDto.ErrorTitle : existingError.ErrorTitle;

            await errorService.UpdateError(existingError);

            return Ok(new ApiResponse(200, "Error updated successfully"));
        }

        [HttpGet]
        [Authorization.AllowAnonymous]
        public async Task<ActionResult<Pagination<ErrorDto>>> GetErrors([FromQuery] ErrorSpecParams errorSpecParams)
        {
            var spec = new AllErrorsSpecification(errorSpecParams);

            var totalItems = await errorRepo.CountAsync(spec);
            var errorItems = await errorRepo.ListAsync(spec);

            var data = Mapper(errorItems);

            return Ok(new Pagination<ErrorDto>(errorSpecParams.PageIndex, errorSpecParams.PageSize, totalItems, data));

        }

        private static List<ErrorDto> Mapper(IReadOnlyList<ErrorEntity> errorEntities)
        {
            var result = new List<ErrorDto>();

            foreach (var errorEntity in errorEntities)
            {
                result.Add(new ErrorDto()
                {
                    ErrorCode = errorEntity.ErrorCode,
                    ErrorDescription = errorEntity.ErrorDescription,
                    ErrorTitle = errorEntity.ErrorTitle,
                    Version = errorEntity.Version,
                    CreatedAt = errorEntity.CreatedAt,
                    UpdatedAt = errorEntity.UpdatedAt,
                    Id = errorEntity.Id
                });
            }

            return result;
        }
    }
}
