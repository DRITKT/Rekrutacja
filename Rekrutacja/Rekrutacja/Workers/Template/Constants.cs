namespace Rekrutacja.Workers.Template
{
    public static class Constants
    {
        public const string SessionName = "ModyfikacjaPracownika";

        // Captions
        public const string ZmiennaXCaption = "Zmienna X";
        public const string ZmiennaYCaption = "Zmienna Y";
        public const string OperacjaCaption = "Operacja";
        public const string DataObliczenCaption = "Data obliczeń";

        // Action
        public const string ActionName = "Kalkulator";
        public const string ActionDescription = "Prosty kalkulator";
        public const int ActionPriority = 10;

        // Operators
        public const string AdditionOperation = "+";
        public const string SubtractionOperation = "-";
        public const string MultiplicationOperation = "*";
        public const string DivisionOperation = "/";

        // Exception messages
        public const string InvalidContextMessage = "Kontekst nie zawiera listy pracowników.";
        public const string NoEmployeesSelectedMessage = "Nie zaznaczono żadnych pracowników.";
        public const string InvalidOperationMessage = "Nieprawidłowa operacja.";
        public const string DivisionByZeroMessage = "Dzielenie przez zero.";

        // Features
        public const string WynikFeature = "Wynik";
        public const string DataObliczenFeature = "DataObliczen";
    }
}
