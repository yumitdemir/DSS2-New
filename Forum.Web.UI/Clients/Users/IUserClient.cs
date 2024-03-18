using Refit;

namespace Forum.Web.UI.Clients.Users
{
    public interface IUserClient
    {
        [Get("/")]
        Task<IEnumerable<UserShortResponse>> GetListAsync();

        [Get("/{userId}")]
        Task<UserDetailsResponse> GetAsync(
            long? userId);

        [Post("/")]
        Task<UserDetailsResponse> CreateAsync(
            [Body] CreateUserRequest user);

        [Put("/{userId}")]
        Task<UserDetailsResponse> UpdateAsync(
            long? userId,
            [Body] UpdateUserRequest user);

        [Delete("/{userId}")]
        Task<UserDetailsResponse> DeleteAsync(
           long? userId);
    }
}
