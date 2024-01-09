using core.Entities;

namespace core.Specifications
{
    public class AllErrorsSpecification : BaseSpecification<ErrorEntity>
    {
        public AllErrorsSpecification(ErrorSpecParams errorSpecParams) : 
            base (x => (string.IsNullOrEmpty(errorSpecParams.Search) || x.ErrorTitle.ToLower().Contains(errorSpecParams.Search)) || x.ErrorDescription.ToLower().Contains(errorSpecParams.Search) || x.ErrorCode.ToLower().Contains(errorSpecParams.Search))  
        {
            AddOrderByDescending(x => x.UpdatedAt);
            ApplyPaging(errorSpecParams.PageSize * (errorSpecParams.PageIndex - 1), errorSpecParams.PageSize);
        }
    }
}
