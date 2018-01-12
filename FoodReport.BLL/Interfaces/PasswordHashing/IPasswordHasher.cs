namespace FoodReport.BLL.Interfaces.PasswordHashing
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}