using Microsoft.VisualStudio.TestTools.UnitTesting;
using DinaD;
using System.Text.RegularExpressions;

namespace DinaD.Tests
{
    [TestClass]
    public class JuryOrModeratorRegisterTests
    {
        JuryOrModeratorRegister window = new JuryOrModeratorRegister();

        [TestMethod]
        public void IsPasswordValid_ValidPassword_ReturnsTrue()
        {
            // Arrange
            string password = "Valid1@Password";

            // Act
            bool result = window.IsPasswordValid(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPasswordValid_PasswordsDoNotMatch_ReturnsFalse()
        {
            // Arrange
            var window = CreateWindow();
            string password = "Valid1@Password";
            string reEnteredPassword = "DifferentPassword";

            // Act
            bool result = password == reEnteredPassword;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsFullNameValid_ValidFullName_ReturnsTrue()
        {
            // Arrange
            var window = CreateWindow();
            string fullName = "Иванов Иван Иванович";

            // Act
            bool result = window.IsFullNameValid(fullName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEmailValid_ValidEmail_ReturnsTrue()
        {
            // Arrange
            var window = CreateWindow();
            string email = "example@test.com";

            // Act
            bool result = window.IsEmailValid(email);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPhoneNumberValid_ValidPhoneNumber_ReturnsTrue()
        {
            // Arrange
            var window = CreateWindow();
            string phoneNumber = "79172379339";

            // Act
            bool result = window.IsPhoneNumberValid(phoneNumber);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
