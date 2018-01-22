using FoodReport.BLL.Services;
using System;
using Xunit;

namespace FoodReport.Tests
{
    public class PasswordHashServiceTests
    {
        private readonly PasswordHashService _hashService;
        public PasswordHashServiceTests()
        {
            _hashService = new PasswordHashService();
        }

        [Fact]
        public void HashPassword_ResultNotNull()
        {
            var pass = "qwerty1234";
            var result = _hashService.HashPassword(pass);
            Assert.NotNull(result);
        }
    }
}
