using System;
using Obligatorisk_Oppgave_1_Universitetssystem._1._User;
using Obligatorisk_Oppgave_1_Universitetssystem.Codes;

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

        // <>-<>-<>-- Oppgave 2 --<>-<>-<>            
            public string Syllabus { get; set; } = string.Empty; // Lagrer pensum for kurset. // Annet navn en "Syllabus"?
            public Dictionary<uint, string> StudentGrades { get; set; } = new Dictionary<uint, string>(); // Lagrer karakter per student-ID. // Dictionary????
        // <>-<>-<>-<>-<>-<>-<>-<>-<>-<>-<>


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

// ================================================================================================
//          ------------------------ OPPGAVE 2: CHECK DUPLICATE COURSE ------------------------
// ================================================================================================
    // Sjekker om kurskode eller kursnavn allerede finnes i systemet.
        bool courseAlreadyExists = allCourses.Any(c =>
            c.CourseCode.Equals(newCourse.CourseCode, StringComparison.OrdinalIgnoreCase) ||
            c.CourseName.Equals(newCourse.CourseName, StringComparison.OrdinalIgnoreCase));

        // Hvis det allerede finnes et kurs med samme kode eller navn, gir en feilmelding og avslutter metoden.
        if (courseAlreadyExists)
        {
            Console.WriteLine("\nA course with the same code or name already exists.");
            return;
        }

            allCourses.Add(newCourse); // Legger kurset til i listen.
            Console.WriteLine($"\nYou have successfully created a new course, named: {newCourse.CourseName}, with code: {newCourse.CourseCode}.");
        }

// ================================================================================================
//          ------------------- SIGN STUDENT UP/OFF COURSE (CASE 2) -------------------
// ================================================================================================
        public static (Student?, Course?) SignStudentUpOrOffCourse(List<Course> allCourses, List<Student> allStudents, Users loggedInUser) // Returnerer både studenten og kurset som ble endret, eller null hvis det oppsto en feil.
        {
            Console.Clear();
            Styles.WriteTitle("--- Sign a student up for or off a Course ---");
             
            if (allCourses.Count == 0) // Sjekker om det finnes noen kurs i
            {
                Console.WriteLine("\nNo courses found.");
                return (null, null); // Returnerer tomt resultat når kurs ikke finnes.
            }

            // --- Finn Studenten som er logget inn ---.
            Student? selectedStudent = allStudents.Find(s =>
                s.Username.Equals(loggedInUser.Username, StringComparison.OrdinalIgnoreCase));

            if (selectedStudent == null) // Sjekker om studenten ble funnet. Hvis ikke, gir en feilmelding og avslutter metoden.
            {
                Console.WriteLine("Student not found.");
                return (null, null); // Returnerer tomt resultat når student ikke finnes.
            }

            Console.WriteLine("\nWhat do you want to do...");
            Console.WriteLine("[1] Sign me up for a course");
            Console.WriteLine("[2] Sign me off a course");
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

            switch (choice)
            {
                case "1": // Meld student på kurs.
                    if (selectedStudent.EnrolledCourses.Contains(selectedCourse)) // Sjekker om studenten allerede er meldt på.
                    {
                        Console.WriteLine("You are already enrolled in this course.");
                        return (null, null);
                    }

                    if (selectedCourse.EnrolledStudents.Count >= selectedCourse.MaxSeats) // Sjekker om kurset er fullt.
                    {
                        Console.WriteLine("Course is full.");
                        return (null, null);
                    }

                    selectedCourse.EnrolledStudents.Add(selectedStudent); // Legger student til i kursets liste.
                    selectedStudent.EnrolledCourses.Add(selectedCourse);  // Legger kurs til i studentens liste.
                    Console.WriteLine($"You have been signed up for {selectedCourse.CourseName}.");
                    return (selectedStudent, selectedCourse);

                case "2": // Meld student av kurs.
                    if (!selectedStudent.EnrolledCourses.Contains(selectedCourse)) // Sjekker om studenten faktisk er meldt på.
                    {
                        Console.WriteLine("You are not enrolled in this course.");
                        return (null, null);
                    }

                    selectedCourse.EnrolledStudents.Remove(selectedStudent); // Fjerner student fra kursets liste.
                    selectedStudent.EnrolledCourses.Remove(selectedCourse);  // Fjerner kurs fra studentens liste.
                    Console.WriteLine($"You have been signed off from {selectedCourse.CourseName}.");
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

// =======================================================================
//      ---------- OPPGAVE 2. REGISTER SYLLABUS ----------
// =======================================================================
    // Lar faglærer registrere pensum for et kurs.
        public static void RegisterSyllabus(List<Course> allCourses)
        {
            Console.Clear();
            Styles.WriteTitle("=== Register Syllabus ===");

            if (allCourses.Count == 0)
            {
                Console.WriteLine("\nNo courses found.");
                return;
            }

            Console.Write("Enter course code to register syllabus for: ");
            string courseCodeInput = Console.ReadLine() ?? string.Empty;

            // --- Finn Kurset ---.
            Course? selectedCourse = allCourses.Find(c =>
                c.CourseCode.Equals(courseCodeInput, StringComparison.OrdinalIgnoreCase));

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.Write("Enter syllabus for the course: ");
            selectedCourse.Syllabus = Console.ReadLine() ?? string.Empty;

            Console.WriteLine($"\nSyllabus for course {selectedCourse.CourseName} has been registered.");
        }

// ================================================================================================
//        ------------------------ OPPGAVE 2: SET STUDENT GRADE ------------------------
// ================================================================================================
    // Lar faglærer sette karakter på en student i et valgt kurs.
        public static void SetStudentGrade(List<Course> allCourses)
        {
            Console.Clear();
            Styles.WriteTitle("--- Set Student Grade ---");

            if (allCourses.Count == 0)
            {
                Console.WriteLine("\nNo courses found.");
                return;
            }

            Console.Write("Enter course code: ");
            string courseCode = Console.ReadLine() ?? string.Empty;

            // --- Finn Kurset ---.
            Course? selectedCourse = allCourses.Find(c =>
                c.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (selectedCourse.EnrolledStudents.Count == 0)
            {
                Console.WriteLine("No students are enrolled in this course.");
                return;
            }

            Console.WriteLine("\nEnrolled students:");
            foreach (Student student in selectedCourse.EnrolledStudents)
            {
                Console.WriteLine($"- {student.Name} (ID: {student.StudentID})");
            }

            Console.Write("\nEnter student ID: ");
            if (!uint.TryParse(Console.ReadLine(), out uint studentId))
            {
                Console.WriteLine("Invalid student ID.");
                return;
            }

            // --- Finn Studenten i det valgte kurset ---.
            Student? selectedStudent = selectedCourse.EnrolledStudents.Find(s => s.StudentID == studentId);

            if (selectedStudent == null)
            {
                Console.WriteLine("Student not found in this course.");
                return;
            }

            // Validerer at karakteren er gyldig (A-F).
            Console.Write("Enter grade (A, B, C, D, E, F): ");
            string grade = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();

            // Sjekker om karakteren er en av de gyldige alternativene. Hvis ikke, gir en feilmelding og avslutter metoden.
            if (grade != "A" && grade != "B" && grade != "C" &&
                grade != "D" && grade != "E" && grade != "F")
            {
                Console.WriteLine("Invalid grade.");
                return;
            }

            // Oppdaterer karakteren for studenten i kursets StudentGrades-dictionary. Hvis studenten allerede har en karakter, vil den bli overskrevet.
            selectedCourse.StudentGrades[selectedStudent.StudentID] = grade;

            Console.WriteLine($"\nGrade {grade} has been set for {selectedStudent.Name} in {selectedCourse.CourseName}.");
        }
    }
}
