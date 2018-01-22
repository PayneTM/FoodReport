using FoodReport.BLL.Services;
using System;
using Xunit;

namespace FoodReport.Tests
{
    public class PasswordHashServiceTests
    {
        public readonly string RightPassword = "Qwerty123456!";
        public readonly string WrongPassword = "Qwerty123456";
        public readonly string RightHash;
        public readonly string TrashHash = "1111111";
        public readonly string InvalidHashWrongAlgorithm = "algorithm:iterations:hashSize:salt:hash";
        public readonly string InvalidHashWrongIterations = "sha1:iterations:hashSize:salt:hash";
        public readonly string InvalidHashIterationNull = "sha1:null:hashSize:salt:hash";
        public readonly string InvalidHashWrongHashSize = "sha1:10000:hashSize:salt:hash";

        public readonly string WrongHash;

        private readonly PasswordHashService _hashService;
        public PasswordHashServiceTests()
        {
            _hashService = new PasswordHashService();
            RightHash = _hashService.HashPassword(RightPassword);
            WrongHash = _hashService.HashPassword(WrongPassword);
        }

        [Fact]
        public void HashPassword_PasswordPassed_ResultNotNull()
        {
            var pass = "qwerty1234";
            var result = _hashService.HashPassword(pass);
            Assert.NotNull(result);
        }
        [Fact]
        public void HashPassword_NullArgument_ArgumentExceptionExpected()
        {
            Assert.Throws<ArgumentException>( () => _hashService.HashPassword(null));
        }
        [Fact]
        public void HashPassword_EmptyStringArgument_ArgumentExceptionExpected()
        {
            Assert.Throws<ArgumentException>( () => _hashService.HashPassword(""));
        }
        [Fact]
        public void HashPassword_WhiteSpaceStringArgument_ArgumentExceptionExpected()
        {
            Assert.Throws<ArgumentException>( () => _hashService.HashPassword(" "));
        }
        [Fact]
        public void ValidatePassword_RightPasswordAndRightHash_TrueExpected()
        {
            var result = _hashService.VerifyPassword(RightPassword, RightHash);
            Assert.True(result);
        }
        [Fact]
        public void ValidatePassword_WrongPasswordAndRightHash_FalseExpected()
        {
            var result = _hashService.VerifyPassword(WrongPassword, RightHash);
            Assert.False(result);
        }
        [Fact]
        public void ValidatePassword_RightPasswordAndWrongHash_FalseExpected()
        {
            var result = _hashService.VerifyPassword(RightPassword, WrongHash);
            Assert.False(result);
        }
        [Fact]
        public void ValidatePassword_PasswordIsNull_ArgumentNullExceptionExpected()
        {
            Assert.ThrowsAny<Exception>(() => _hashService.VerifyPassword(null, RightHash));
        }
        [Fact]
        public void ValidatePassword_HashIsNullPassed_NullReferenceExceptionExpected()
        {
            Assert.Throws<NullReferenceException>(() => _hashService.VerifyPassword(RightPassword, null));
        }
        [Fact]
        public void ValidatePassword_NullArguments_NullReferenceExceptionExpected()
        {
            Assert.Throws<NullReferenceException>(() => _hashService.VerifyPassword(null, null));
        }
        [Fact]
        public void ValidatePassword_InvalidHashPassed_InvalidHashExceptionExpected()
        {
            Assert.Throws<InvalidHashException>(() => _hashService.VerifyPassword(RightPassword, TrashHash));
        }

        [Fact]
        public void ValidatePassword_InvalidHashWrongAlgorithmArgument_CannotPerformOperationExceptionExpected()
        {
            Assert.Throws<CannotPerformOperationException> ( () => _hashService.VerifyPassword(RightPassword,InvalidHashWrongAlgorithm) );
        }
        [Fact]
        public void ValidatePassword_InvalidHashWrongIterationsArgument_InvalidHashExceptionExpected()
        {
            Assert.Throws<InvalidHashException> ( () => _hashService.VerifyPassword(RightPassword,InvalidHashWrongIterations) );
        }
        [Fact]
        public void ValidatePassword_InvalidHashWrongHashSizeArgument_InvalidHashExceptionExpected()
        {
            Assert.Throws<InvalidHashException> ( () => _hashService.VerifyPassword(RightPassword,InvalidHashWrongHashSize) );
        }
        [Fact]
        public void ValidatePassword_IterationNullArgument_InvalidHashExceptionExpected()
        {
            Assert.Throws<InvalidHashException> ( () => _hashService.VerifyPassword(RightPassword,InvalidHashIterationNull) );
        }
    }
}
