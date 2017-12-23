namespace FoodReport.Common.Interfaces
{
    public interface IUser
    {
        string Id { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Role { get; set; }
    }
}
