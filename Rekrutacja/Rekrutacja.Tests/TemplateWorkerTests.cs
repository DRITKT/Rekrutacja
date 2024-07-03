using NUnit.Framework;
using Rekrutacja.Workers.Template;
using Rekrutacja.Workers.Template.Strategies;
using System;

namespace Rekrutacja.Tests
{
    [TestFixture]
    public class TemplateWorkerTests
    {
        [Test]
        public void ObliczWynik_Dodawanie_ZwracaPoprawnyWynik()
        {
            var worker = new TemplateWorker();
            var parametry = new TemplateWorker.TemplateWorkerParametry(null)
            {
                ZmiennaX = 3,
                ZmiennaY = 2,
                Operacja = Constants.AdditionOperation
            };
            worker.Parametry = parametry;
            var wynik = worker.ObliczWynik();
            Assert.AreEqual(5, wynik);
        }

        [Test]
        public void ObliczWynik_Odejmowanie_ZwracaPoprawnyWynik()
        {
            var worker = new TemplateWorker();
            var parametry = new TemplateWorker.TemplateWorkerParametry(null)
            {
                ZmiennaX = 5,
                ZmiennaY = 3,
                Operacja = Constants.SubtractionOperation
            };
            worker.Parametry = parametry;
            var wynik = worker.ObliczWynik();
            Assert.AreEqual(2, wynik);
        }

        [Test]
        public void ObliczWynik_Mnozenie_ZwracaPoprawnyWynik()
        {
            var worker = new TemplateWorker();
            var parametry = new TemplateWorker.TemplateWorkerParametry(null)
            {
                ZmiennaX = 4,
                ZmiennaY = 3,
                Operacja = Constants.MultiplicationOperation
            };
            worker.Parametry = parametry;
            var wynik = worker.ObliczWynik();
            Assert.AreEqual(12, wynik);
        }

        [Test]
        public void ObliczWynik_Dzielenie_ZwracaPoprawnyWynik()
        {
            var worker = new TemplateWorker();
            var parametry = new TemplateWorker.TemplateWorkerParametry(null)
            {
                ZmiennaX = 6,
                ZmiennaY = 2,
                Operacja = Constants.DivisionOperation
            };
            worker.Parametry = parametry;
            var wynik = worker.ObliczWynik();
            Assert.AreEqual(3, wynik);
        }

        [Test]
        public void ObliczWynik_DzieleniePrzezZero_ThrowsDivideByZeroException()
        {
            var worker = new TemplateWorker();
            var parametry = new TemplateWorker.TemplateWorkerParametry(null)
            {
                ZmiennaX = 3,
                ZmiennaY = 0,
                Operacja = Constants.DivisionOperation
            };
            worker.Parametry = parametry;
            Assert.Throws<DivideByZeroException>(() => worker.ObliczWynik());
        }

        [Test]
        public void ObliczWynik_NieznanaOperacja_ThrowsInvalidOperationException()
        {
            var worker = new TemplateWorker();
            var parametry = new TemplateWorker.TemplateWorkerParametry(null)
            {
                ZmiennaX = 3,
                ZmiennaY = 2,
                Operacja = "%"
            };
            worker.Parametry = parametry;
            Assert.Throws<InvalidOperationException>(() => worker.ObliczWynik());
        }

        [Test]
        public void GetStrategy_Dodawanie_ZwracaAdditionStrategy()
        {
            var worker = new TemplateWorker();
            var strategy = worker.GetStrategy(Constants.AdditionOperation);
            Assert.IsInstanceOf<AdditionStrategy>(strategy);
        }

        [Test]
        public void GetStrategy_Odejmowanie_ZwracaSubtractionStrategy()
        {
            var worker = new TemplateWorker();
            var strategy = worker.GetStrategy(Constants.SubtractionOperation);
            Assert.IsInstanceOf<SubtractionStrategy>(strategy);
        }

        [Test]
        public void GetStrategy_Mnozenie_ZwracaMultiplicationStrategy()
        {
            var worker = new TemplateWorker();
            var strategy = worker.GetStrategy(Constants.MultiplicationOperation);
            Assert.IsInstanceOf<MultiplicationStrategy>(strategy);
        }

        [Test]
        public void GetStrategy_Dzielenie_ZwracaDivisionStrategy()
        {
            var worker = new TemplateWorker();
            var strategy = worker.GetStrategy(Constants.DivisionOperation);
            Assert.IsInstanceOf<DivisionStrategy>(strategy);
        }

        [Test]
        public void GetStrategy_NieznanaOperacja_ThrowsInvalidOperationException()
        {
            var worker = new TemplateWorker();
            Assert.Throws<InvalidOperationException>(() => worker.GetStrategy("%"));
        }
    }
}
