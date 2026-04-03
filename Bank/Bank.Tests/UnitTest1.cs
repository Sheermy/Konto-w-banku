using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Bank.Tests
{
    [TestClass]
    public class KontoTests
    {
        [TestMethod]
        public void Test_Wplata_Poprawna()
        {
            var konto = new Konto("Uzytkownik", 100);
            konto.Wplata(50);
            Assert.AreEqual(150, konto.Bilans);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_Wplata_Zablokowane()
        {
            var konto = new Konto("Uzytkownik", 100);
            konto.BlokujKonto();
            konto.Wplata(50);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_Wyplata_BrakSrodkow()
        {
            var konto = new Konto("Uzytkownik", 100);
            konto.Wyplata(200);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Wplata_Ujemna()
        {
            var konto = new Konto("Uzytkownik", 100);
            konto.Wplata(-10);
        }

        [TestMethod]
        public void Test_Blokada_Status()
        {
            var konto = new Konto("Uzytkownik");
            konto.BlokujKonto();
            Assert.IsTrue(konto.Zablokowane);
            konto.OdblokujKonto();
            Assert.IsFalse(konto.Zablokowane);
        }
    }
}