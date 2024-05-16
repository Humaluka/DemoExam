using DinaD;
namespace TestProject5
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            JuryOrModeratorRegister juryOrModeratorRegister = new JuryOrModeratorRegister();
            string pass = "uedjjuked";
            bool result = juryOrModeratorRegister.IsEmailValid(pass);
        }
    }
}