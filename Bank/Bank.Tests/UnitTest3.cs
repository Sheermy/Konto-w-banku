using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class KontoLimitTests
    {
        [TestMethod]
        public void Test_Wyplata_Debetowa_ZasadyDelegacji()
        {
            var konto = new KontoLimit("Testowy", 100, 50);
            konto.Wyplata(120);
            Assert.AreEqual(30, konto.Bilans);
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void Test_Wplata_PoDebecie_Odblokowanie()
        {
            var konto = new KontoLimit("Testowy", 100, 50);
            konto.Wyplata(120);
            konto.Wplata(30);
            Assert.IsFalse(konto.Zablokowane);
            Assert.AreEqual(60, konto.Bilans);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_Wyplata_ZablokowaneKonto_Delegacja()
        {
            var konto = new KontoLimit("Testowy", 100);
            konto.BlokujKonto();
            konto.Wyplata(10);
        }

        [TestMethod]
        public void Test_Nazwa_Pobierana_Z_WewnetrznegoObiektu()
        {
            var konto = new KontoLimit("Jan", 0);
            Assert.AreEqual("Jan", konto.Nazwa);
        }
    }
}