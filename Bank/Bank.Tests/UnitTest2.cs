using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;
using System;

namespace Bank.Tests
{
    [TestClass]
    public class KontoPlusTests
    {
        [TestMethod]
        public void Test_Wyplata_Z_Debetem_BlokujeKonto()
        {
            var konto = new KontoPlus("Uzytkownik", 100, 50);
            konto.Wyplata(130);
            Assert.AreEqual(20, konto.Bilans);
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void Test_Wplata_SplataDebetu_OdblokowujeKonto()
        {
            var konto = new KontoPlus("Uzytkownik", 100, 50);
            konto.Wyplata(130);
            konto.Wplata(40);
            Assert.IsFalse(konto.Zablokowane);
            Assert.AreEqual(10, konto.Bilans - 50);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_Wyplata_PrzekroczenieLimitu_RzucaWyjatek()
        {
            var konto = new KontoPlus("Uzytkownik", 100, 50);
            konto.Wyplata(160);
        }

        [TestMethod]
        public void Test_Bilans_UwzgledniaLimit()
        {
            var konto = new KontoPlus("Uzytkownik", 100, 50);
            Assert.AreEqual(150, konto.Bilans);
        }
    }
}