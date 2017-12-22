using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.BLL.Interfaces.PasswordHashing
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
