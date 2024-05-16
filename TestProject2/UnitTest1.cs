using Microsoft.VisualStudio.TestTools.UnitTesting;
using DinaD;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace DinaD.Tests
{
    [TestClass]
    public class JuryOrModeratorRegisterTests
    {
        private JuryOrModeratorRegister _registerWindow;

        [TestInitialize]
        public void Setup()
        {
            _registerWindow = new JuryOrModeratorRegister();
        }

        [TestMethod]
        public void IsPasswordValid_ShouldReturnTrue_ForValidPassword()
        {
            // Arrange
            string validPassword = "Valid1!";

            // Act
            bool result = _registerWindow.IsPasswordValid(validPassword);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsEmailValid_ShouldReturnTrue_ForValidEmail()
        {
            // Arrange
            string validEmail = "test@example.com";

            // Act
            bool result = _registerWindow.IsEmailValid(validEmail);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPhoneNumberValid_ShouldReturnTrue_ForValidPhoneNumber()
        {
            // Arrange
            string validPhoneNumber = "79172379339";

            // Act
            bool result = _registerWindow.IsPhoneNumberValid(validPhoneNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFullNameValid_ShouldReturnTrue_ForValidFullName()
        {
            // Arrange
            string validFullName = "Иванов Иван Иванович";

            // Act
            bool result = _registerWindow.IsFullNameValid(validFullName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OkButton_Click_ShouldRegisterUser_WithValidData()
        {
            // Arrange
            _registerWindow.FullNameTextBox.Text = "Иванов Иван Иванович";
            _registerWindow.EmailTextBox.Text = "test@example.com";
            _registerWindow.PhoneNumberTextBox.Text = "79172379339";
            _registerWindow.PasswordBox.Password = "Valid1!";
            _registerWindow.ReEnterPasswordBox.Password = "Valid1!";
            _registerWindow.SexComboBox.SelectedValue = 1;
            _registerWindow.CountryComboBox.SelectedValue = 1;
            _registerWindow.FieldComboBox.SelectedValue = 1;
            _registerWindow.RoleComboBox.SelectedItem = new ComboBoxItem { Content = "Жюри", Tag = 1 };
            _registerWindow.IdNumberTextBox.Text = "12345";
            _registerWindow.UserImagesComboBox.SelectedItem = "foto1.jpg";

            // Mocking necessary properties and methods for simplicity
            _registerWindow._context = new praktikaVesnaEntities
            {
                People = new List<Person>().AsQueryable()
            };

            // Act
            _registerWindow.OkButton_Click(null, null);

            // Assert
            var registeredPerson = _registerWindow._context.People.FirstOrDefault(p => p.Id == 12345);
            Assert.IsNotNull(registeredPerson);
            Assert.AreEqual("Иванов Иван Иванович", registeredPerson.Name);
            Assert.AreEqual("test@example.com", registeredPerson.EMail);
            Assert.AreEqual("79172379339", registeredPerson.PhoneNumber);
        }
    }

    // Mocking the necessary parts of the praktikaVesnaEntities for testing
    public class praktikaVesnaEntities
    {
        public IQueryable<Person> People { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int SexId { get; set; }
        public int RoleId { get; set; }
        public int DirectionId { get; set; }
        public int CountryId { get; set; }
        public string Image { get; set; }
    }
}
