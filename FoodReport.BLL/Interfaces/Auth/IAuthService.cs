using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.IO;

namespace FoodReport.BLL.Interfaces.Auth
{
    public interface IAuthService
    {
        void Login(ILoginModel model);
        void Registration();
    }
}
