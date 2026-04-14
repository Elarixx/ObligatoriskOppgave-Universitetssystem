using System;
using System.Collections.Generic;
using System.Text;

namespace Oppgave_2.Codes._1._Users
{
    internal enum UserType
    {
        Student = 1,
        Teacher = 2,
        Librarian = 3
    }

    internal abstract class User // Hvorfor abstract?
    {
        public uint UserID { get; set; } = (uint)Random.Shared.Next(1, 9999);
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserType Type { get; protected set; }
    }
}
