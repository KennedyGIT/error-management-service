using core.Entities;

namespace core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserEntity user);
    }
}
