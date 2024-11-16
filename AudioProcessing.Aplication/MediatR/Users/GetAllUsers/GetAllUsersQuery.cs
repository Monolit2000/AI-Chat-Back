using MediatR;

namespace AudioProcessing.Aplication.MediatR.Users.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }
}
