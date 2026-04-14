using System;
using System.Collections.Generic;
using System.Text;

namespace Oppgave_2.Codes._1._Users
{
    internal class Teacher : User
    {
        public Teacher()
        {
            Type = UserType.Teacher; // Setter brukerens type til Teacher når en ny Teacher opprettes.
        }
    }
}
