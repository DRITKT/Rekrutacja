using Rekrutacja.Workers.Template;
using Rekrutacja.Workers.Template.Interfaces;
using Rekrutacja.Workers.Template.Strategies;
using Soneta.Business;
using Soneta.Kadry;
using Soneta.Types;
using System;

[assembly: Worker(typeof(TemplateWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.Template
{
    public class TemplateWorker
    {
        public class TemplateWorkerParametry : ContextBase
        {
            [Caption(Constants.ZmiennaXCaption)]
            public double ZmiennaX { get; set; }

            [Caption(Constants.ZmiennaYCaption)]
            public double ZmiennaY { get; set; }

            [Caption(Constants.OperacjaCaption)]
            public string Operacja { get; set; }

            [Caption(Constants.DataObliczenCaption)]
            public Date DataObliczen { get; set; }

            public TemplateWorkerParametry(Context context) : base(context)
            {
                DataObliczen = Date.Today;
            }
        }

        [Context]
        public Context Cx { get; set; }

        [Context]
        public TemplateWorkerParametry Parametry { get; set; }

        [Action(Constants.ActionName,
           Description = Constants.ActionDescription,
           Priority = Constants.ActionPriority,
           Mode = ActionMode.ReadOnlySession,
           Icon = ActionIcon.Accept,
           Target = ActionTarget.ToolbarWithText)]
        public void WykonajAkcje()
        {
            DebuggerSession.MarkLineAsBreakPoint();

            var pracownicy = PobierzPracownikow();
            var wynik = ObliczWynik();

            ModyfikujPracownikow(pracownicy, wynik);
        }

        public Pracownik[] PobierzPracownikow()
        {
            if (!Cx.Contains(typeof(Pracownik[])))
            {
                throw new InvalidOperationException(Constants.InvalidContextMessage);
            }

            var pracownicy = Cx[typeof(Pracownik[])] as Pracownik[];
            if (pracownicy == null || pracownicy.Length == 0)
            {
                throw new InvalidOperationException(Constants.NoEmployeesSelectedMessage);
            }

            return pracownicy;
        }

        public double ObliczWynik()
        {
            ICalculationStrategy strategy = GetStrategy(Parametry.Operacja);
            return strategy.Calculate(Parametry.ZmiennaX, Parametry.ZmiennaY);
        }

        public ICalculationStrategy GetStrategy(string operation)
        {
            switch (operation)
            {
                case Constants.AdditionOperation:
                    return new AdditionStrategy();
                case Constants.SubtractionOperation:
                    return new SubtractionStrategy();
                case Constants.MultiplicationOperation:
                    return new MultiplicationStrategy();
                case Constants.DivisionOperation:
                    return new DivisionStrategy();
                default:
                    throw new InvalidOperationException(Constants.InvalidOperationMessage);
            }
        }

        public void ModyfikujPracownikow(Pracownik[] pracownicy, double wynik)
        {
            using (var nowaSesja = Cx.Login.CreateSession(false, false, Constants.SessionName))
            {
                using (var trans = nowaSesja.Logout(true))
                {
                    foreach (var pracownik in pracownicy)
                    {
                        var pracownikZSesja = nowaSesja.Get(pracownik);
                        UstawCechy(pracownikZSesja, wynik);
                    }
                    trans.CommitUI();
                }
                nowaSesja.Save();
            }
        }

        public void UstawCechy(Pracownik pracownik, double wynik)
        {
            pracownik.Features[Constants.WynikFeature] = wynik;
            pracownik.Features[Constants.DataObliczenFeature] = Parametry.DataObliczen;
        }
    }
}
