using System;

namespace Obligatorisk_Oppgave_1_Universitetssystem._3._Library
{
    internal class Books
    {
        public uint BookID { get; init; } = (uint)Random.Shared.Next();
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }
    }
}
