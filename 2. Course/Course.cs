using System;
using Obligatorisk_Oppgave_1_Universitetssystem._1._User;

namespace Obligatorisk_Oppgave_1_Universitetssystem._2._Course
{
    // ================================================
    //    ----------- COURSE CLASS -----------
    // ================================================
    internal class Course
    {
        // ================================================
        //     ----------- COURSE PROPERTIES -----------
        // ================================================
            public string CourseCode { get; set; } = string.Empty; // Unik kode for kurset.
            public string CourseName { get; set; } = string.Empty; // Navnet på kurset.
            public int CourseCredits { get; set; }                 // Antall studiepoeng.
            public int MaxSeats { get; set; }                      // Maks kapasitet.

            public List<Student> EnrolledStudents { get; set; } = new List<Student>(); // Liste over studenter som er meldt på kurset.

// ================================================================================================
//              ------------------- CREATE COURSE (CASE 1) -------------------
// ================================================================================================
    // Metode for å opprette et nytt kurs og legge det til i listen over alle kurs.
        public static void CreateCourse(List<Course> allCourses)
        {
            Console.Clear();
            Styles.WriteTitle("--- Create a new Course ---");

            Course newCourse = new Course(); // Lager nytt kursobjekt.

            Console.Write("\nEnter course name: ");
            newCourse.CourseName = Console.ReadLine() ?? string.Empty; // Leser kursnavn fra brukeren, og bruker tom streng hvis input er null.

            Console.Write("Enter course code: ");
            newCourse.CourseCode = Console.ReadLine() ?? string.Empty; // Leser kurskode fra brukeren, og bruker tom streng hvis input er null.

            Console.Write("Enter course credits: ");
            if (!int.TryParse(Console.ReadLine(), out int courseCredits)) // Leser studiepoeng fra brukeren og prøver å konvertere til int. Hvis det feiler, gir en feilmelding og avslutter metoden.
            {
                Console.WriteLine("Invalid course credits input!");
                return;
            }
            newCourse.CourseCredits = courseCredits; // Setter studiepoeng for kurset.

            Console.Write("Enter available seats: ");
            if (!int.TryParse(Console.ReadLine(), out int maxSeats)) // Leser maks antall plasser fra brukeren og prøver å konvertere til int. Hvis det feiler, gir en feilmelding og avslutter metoden.
            {
                Console.WriteLine("Invalid seats input!");
                return;
            }
            newCourse.MaxSeats = maxSeats; // Setter maks antall plasser for kurset.

            allCourses.Add(newCourse); // Legger kurset til i listen.
            Console.WriteLine($"\nYou have successfully created a new course, named: {newCourse.CourseName}, with code: {newCourse.CourseCode}.");
        }

// ================================================================================================
//          ------------------- SIGN STUDENT UP/OFF COURSE (CASE 2) -------------------
// ================================================================================================
        public static (Student?, Course?) SignStudentUpOrOffCourse(List<Course> allCourses, List<Student> allStudents) // Returnerer både studenten og kurset som ble endret, eller null hvis det oppsto en feil.
        {
            Console.Clear();
            Styles.WriteTitle("--- Sign a student up for or off a Course ---");
             
            if (allCourses.Count == 0) // Sjekker om det finnes noen kurs i
            {
                Console.WriteLine("\nNo courses found.");
                return (null, null); // Returnerer tomt resultat når kurs ikke finnes.
            }

            Console.WriteLine("\nWhat do you want to do...");
            Console.WriteLine("[1] Sign student up for a course");
            Console.WriteLine("[2] Sign student off a course");
            Console.Write("\nI choose: ");
            string? choice = Console.ReadLine();

       // --- Finn Kurset ---.
            Console.Write("Enter course code: ");
            string userGivesCodeInput = Console.ReadLine() ?? string.Empty; // Leser kurskode fra brukeren, og bruker tom streng hvis input er null.

            Course? selectedCourse = allCourses.Find(c =>
                c.CourseCode.Equals(userGivesCodeInput, StringComparison.OrdinalIgnoreCase)); // Sammenligner uten forskjell på store/små bokstaver.

            if (selectedCourse == null) // Sjekker om kurset ble funnet. Hvis ikke, gir en feilmelding og avslutter metoden.
            {
                Console.WriteLine("Course not found.");
                return (null, null);
            }

       // --- Finn Studenten ---.
            Console.Write("Enter student ID: ");
            if (!uint.TryParse(Console.ReadLine(), out uint userGivesStudentIDInput)) // Leser student-ID fra brukeren og prøver å konvertere til uint. Hvis det feiler, gir en feilmelding og avslutter metoden.
            {
                Console.WriteLine("Invalid student ID input!");
                return (null, null); // Returnerer tomt resultat når student-ID ikke er gyldig.
            }

            Student? selectedStudent = allStudents.Find(s => s.StudentID == userGivesStudentIDInput); // Sammenligner student-ID for å finne riktig student.

            if (selectedStudent == null) // Sjekker om studenten ble funnet. Hvis ikke, gir en feilmelding og avslutter metoden.
            {
                Console.WriteLine("Student not found.");
                return (null, null); // Returnerer tomt resultat når student ikke finnes.
            }

            switch (choice)
            {
                case "1": // Meld student på kurs.
                    if (selectedStudent.EnrolledCourses.Contains(selectedCourse)) // Sjekker om studenten allerede er meldt på.
                    {
                        Console.WriteLine("Student is already enrolled in this course.");
                        return (null, null);
                    }

                    if (selectedCourse.EnrolledStudents.Count >= selectedCourse.MaxSeats) // Sjekker om kurset er fullt.
                    {
                        Console.WriteLine("Course is full.");
                        return (null, null);
                    }

                    selectedCourse.EnrolledStudents.Add(selectedStudent); // Legger student til i kursets liste.
                    selectedStudent.EnrolledCourses.Add(selectedCourse);  // Legger kurs til i studentens liste.
                    return (selectedStudent, selectedCourse);

                case "2": // Meld student av kurs.
                    if (!selectedStudent.EnrolledCourses.Contains(selectedCourse)) // Sjekker om studenten faktisk er meldt på.
                    {
                        Console.WriteLine("Student is not enrolled in this course.");
                        return (null, null);
                    }

                    selectedCourse.EnrolledStudents.Remove(selectedStudent); // Fjerner student fra kursets liste.
                    selectedStudent.EnrolledCourses.Remove(selectedCourse);  // Fjerner kurs fra studentens liste.
                    return (selectedStudent, selectedCourse); // Returnerer både studenten og kurset som ble endret, slik at det kan brukes i loggmeldingen i Program.cs.

                default:
                    Console.WriteLine("Invalid choice!");
                    return (null, null);
            }
        }

// ================================================================================================
//          ------------------- PRINT KURS OG STUDENT (CASE 3) -------------------
// ================================================================================================
    // Metode for å skrive ut alle kurs og de studentene som er meldt på hvert kurs.
        public static void PrintCourseAndStudent(List<Course> allCourses) // Skriver ut alle kurs og de studentene som er meldt på hvert kurs.
        {
            Console.Clear();
            Styles.WriteTitle("--- Print Course and Enrolled Students ---");

            if (allCourses.Count == 0) // Sjekker om det finnes noen kurs i listen. Hvis ikke, gir en melding og avslutter metoden.
            {
                Console.WriteLine("\nNo courses found.");
                return;
            }

            foreach (Course course in allCourses) // Går gjennom hvert kurs i listen.
            {
                Console.WriteLine($"Course: {course.CourseName} ({course.CourseCode})");
                Console.WriteLine($"Credits: {course.CourseCredits}");
                Console.WriteLine($"Seats: {course.EnrolledStudents.Count}/{course.MaxSeats}");

                if (course.EnrolledStudents.Count == 0) // Sjekker om det er noen studenter meldt på kurset. Hvis ikke, skriver ut at det ikke er noen studenter og fortsetter til neste kurs.
                {
                    Console.WriteLine("Students: none"); // Ingen studenter er meldt på dette kurset.
                    continue;
                }

                Console.WriteLine("Students:");
                foreach (Student student in course.EnrolledStudents) // Går gjennom hver student som er meldt på kurset og skriver ut deres navn, ID og e-post.
                {
                    Console.WriteLine($"- {student.Name} (ID: {student.StudentID}, Email: {student.Email})");
                }
            }
        }

// ================================================================================================
//          ------------------------ SEARCH COURSE (CASE 4) ------------------------
// ================================================================================================
    // Metode for å søke etter kurs basert på kurskode eller kursnavn, og skriver ut treffene.
        public static void SearchCourse(List<Course> allCourses) // Lar brukeren søke etter kurs basert på kurskode eller kursnavn, og skriver ut treffene.
        {
            Console.Clear();
            Styles.WriteTitle("--- Search Course ---");

            if (allCourses.Count == 0) // Sjekker om det finnes noen kurs i listen. Hvis ikke, gir en melding og avslutter metoden.
            {
                Console.WriteLine("\nNo courses found.");
                return;
            }

            Console.WriteLine("\nSearch by...");
            Console.WriteLine("[1] Course code");
            Console.WriteLine("[2] Course name");
            Console.Write("\nI choose: ");
            string? choice = Console.ReadLine();

            List<Course> results = new List<Course>(); // Liste for å lagre kurs som matcher søket.

            switch (choice) 
            {
                case "1":
                    Console.Write("Enter course code: ");
                    string codeInput = Console.ReadLine() ?? string.Empty; // Bruker tom streng hvis input er null.
                    results = allCourses.FindAll(c =>
                        c.CourseCode.Equals(codeInput, StringComparison.OrdinalIgnoreCase)); // Eksakt match på kurskode, uten forskjell på store/små bokstaver.
                    break;

                case "2":
                    Console.Write("Enter course name: ");
                    string nameInput = Console.ReadLine() ?? string.Empty; // Bruker tom streng hvis input er null.
                    results = allCourses.FindAll(c =>
                        c.CourseName.Contains(nameInput, StringComparison.OrdinalIgnoreCase)); // Delvis match på kursnavn, uten forskjell på store/små bokstaver.
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    return;
            }

            if (results.Count == 0) // Sjekker om det ble funnet noen kurs som matcher søket. Hvis ikke, gir en melding og avslutter metoden.
            {
                Console.WriteLine("No matching courses found.");
                return;
            }

            Console.WriteLine("\nMatching Courses:");
            foreach (Course course in results) // Går gjennom de kursene som matcher søket og skriver ut deres navn, kode, studiepoeng og antall plasser.
            {
                Console.WriteLine($"- {course.CourseName} ({course.CourseCode}), Credits: {course.CourseCredits}, Seats: {course.EnrolledStudents.Count}/{course.MaxSeats}");
            }
        }

    // ================================================
    //    ----------- DEFAULT COURSES -----------
    // ================================================
        // Metode for å generere en liste med forhåndsdefinerte kurs.
            public static List<Course> DefaultCourses()
            {
                return new List<Course>
                {
                    new Course { CourseName = "Intro to AI", CourseCode = "IAI-2026", CourseCredits = 5, MaxSeats = 50 },
                    new Course { CourseName = "Greener Environment", CourseCode = "GE-2026", CourseCredits = 10, MaxSeats = 20 }
                };
            }
    }
}
