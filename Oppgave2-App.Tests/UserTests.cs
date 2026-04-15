using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obligatorisk_Oppgave_1_Universitetssystem._1._User;
using Obligatorisk_Oppgave_1_Universitetssystem.Codes;

namespace Oppgave2_App.Tests
{
    [TestClass]
    public sealed class UserTests
    {
        // ================================================
        //    ----------- TEST SETUP -----------
        // ================================================
        // Lager en UserManager med standardbrukere for testene.
        private static UserManager CreateUserManager()
        {
            List<Student> students = Student.DefaultStudents();
            List<Employee> employees = Employee.DefaultEmployees();

            return new UserManager(students, employees);
        }

        // ================================================
        //    ----------- LOGIN TESTS -----------
        // ================================================
        [TestMethod]
        public void LoginOk()
        {
            UserManager userManager = CreateUserManager();

            Users? user = userManager.Login("SaylorSinclair", "Sinclair_123");

            Assert.IsNotNull(user);
            Assert.AreEqual("Saylor Sinclair", user.Name);
        }

        [TestMethod]
        public void LoginWrongPassword()
        {
            UserManager userManager = CreateUserManager();

            Users? user = userManager.Login("SaylorSinclair", "WrongPassword");

            Assert.IsNull(user);
        }

        // ================================================
        //    ----------- REGISTER TESTS -----------
        // ================================================
        [TestMethod]
        public void RegisterDuplicateUser()
        {
            UserManager userManager = CreateUserManager();

            Student newStudent = new Student
            {
                Name = "Test Student",
                Email = "test@student.com",
                Username = "SaylorSinclair",
                Password = "Test123",
                Role = UserRole.Student
            };

            bool result = userManager.RegisterUser(newStudent, out string message);

            Assert.IsFalse(result);
            Assert.AreEqual("Username already exists. Please choose a different username.", message);
        }

        [TestMethod]
        public void RegisterNewUser()
        {
            UserManager userManager = CreateUserManager();

            Student newStudent = new Student
            {
                Name = "New Student",
                Email = "new@student.com",
                Username = "NewUniqueUser",
                Password = "Password123",
                Role = UserRole.Student
            };

            bool result = userManager.RegisterUser(newStudent, out string message);

            Assert.IsTrue(result);
            Assert.AreEqual("User registered successfully.", message);
        }
    }
}
