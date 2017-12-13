using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.BLL.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
