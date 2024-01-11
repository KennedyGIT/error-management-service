using core.Entities;

namespace core.Specifications
{
    public class ErrorCodeSpecification : BaseSpecification<ErrorEntity>
    {
        public ErrorCodeSpecification(string errorCode) :
            base(x => (x.ErrorCode.ToLower().Contains(errorCode)))
        {
        }
    }
}
