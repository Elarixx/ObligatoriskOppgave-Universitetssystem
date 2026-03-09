using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._1._User
{
    // Denne klassen er en abstrakt klasse som fungerer som en base for både Student og Employee klasser.
    internal abstract class Users
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
