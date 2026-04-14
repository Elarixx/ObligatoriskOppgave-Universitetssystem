using System;
using System.Collections.Generic;
using System.Text;

namespace Oppgave_2.Codes
{
    internal class STYLE
    {

    // ================================================
    //
    // ================================================
        // Spør bruker omm de vil prøve igjen på samme handling.
        public static bool AskToRetryCurrentAction()
        {
            Console.Write("\nDo you want to try again? (y/n): ");
            string retryChoice = Console.ReadLine() ?? string.Empty;

            // Returnerer true hvis brukeren skriver 'y' eller 'yes' (case-insensitive).
            return retryChoice.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                   retryChoice.Equals("yes", StringComparison.OrdinalIgnoreCase);
        }

    // ================================================
    //          GJØR ENDRINGER HER!!!!
    // ================================================
        // 
        public static void PauseBeforeMenu(bool running)
        {
            if (running)
            {
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }
}
