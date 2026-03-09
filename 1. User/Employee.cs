using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._1._User
{
    internal class Employee : Users 
    {
        public uint EmployeeID { get; init; } = (uint)Random.Shared.Next(); 
        public string Position { get; set; }
        public string Department { get; set; }

    }
}
