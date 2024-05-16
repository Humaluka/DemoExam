using DinaD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PassTest0()
        {

            string pass = "DINA12";
            bool extended  = false;
            bool actual = JuryOrModeratorRegister.IsPasswordValid(pass);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void PassTest1()
        {

            string pass = "Dina";
            bool extended = false;
            bool actual = JuryOrModeratorRegister.IsPasswordValid(pass);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void PassTest2()
        {

            string pass = "DinaDina123";
            bool extended = false;
            bool actual = JuryOrModeratorRegister.IsPasswordValid(pass);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void PassTest3()
        {

            string pass = "dInAr6_";
            bool extended = true;
            bool actual = JuryOrModeratorRegister.IsPasswordValid(pass);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void PassTest4()
        {

            string pass = "123456dinaDINA_";
            bool extended = true;
            bool actual = JuryOrModeratorRegister.IsPasswordValid(pass);
            Assert.AreEqual(extended, actual);

        }



        [TestMethod]
        public void PhoneNumberTest0()
        {

            string phone = "12345678910";
            bool extended = false;
            bool actual = JuryOrModeratorRegister.IsPhoneNumberValid(phone);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void PhoneNumberTest1()
        {

            string phone = "79969521623";
            bool extended = true;
            bool actual = JuryOrModeratorRegister.IsPhoneNumberValid(phone);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void PhoneNumberTest2()
        {

            string phone = "79969521623759";
            bool extended = false;
            bool actual = JuryOrModeratorRegister.IsPhoneNumberValid(phone);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void EmailTest()
        {
            string email = "fdfd";
            bool extended = false;
            bool actual = JuryOrModeratorRegister.IsEmailValid(email);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void EmailTest1()
        {

            string email = "fdfd@mail.ru";
            bool extended = true;
            bool actual = JuryOrModeratorRegister.IsEmailValid(email);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void FullNameTest()
        {

            string fullname = "Фамилия";
            bool extended = false;
            bool actual = JuryOrModeratorRegister.IsFullNameValid(fullname);
            Assert.AreEqual(extended, actual);

        }
        [TestMethod]
        public void FullNameTest1()
        {

            string fullname = "Фамилия Имя";
            bool extended = true;
            bool actual = JuryOrModeratorRegister.IsFullNameValid(fullname);
            Assert.AreEqual(extended, actual);

        }
    }
}
