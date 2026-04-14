using System;
using System.Collections.Generic;
using System.Text;

namespace Oppgave_2.Codes._1._Users
{
    internal class Librarian : User
    {
        public Librarian()
        {
            Type = UserType.Librarian; // Setter brukerens type til Librarian når en ny Librarian opprettes.
        }
    }
}
