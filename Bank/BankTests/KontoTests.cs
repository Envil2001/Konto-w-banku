using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace BankTests
{
    [TestClass]
    public class KontoTests
    {
        [TestMethod]
        public void Wplata_PozytywnaKwota_ZwiekszaBilans()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wplata(50);
            Assert.AreEqual(150, konto.Bilans);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Wplata_UjemnaKwota_RzucaWyjatek()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wplata(-50);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wplata_KontoZablokowane_RzucaWyjatek()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.BlokujKonto();
            konto.Wplata(50);
        }

        [TestMethod]
        public void Wyplata_PrawidlowaKwota_ZmniejszaBilans()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wyplata(50);
            Assert.AreEqual(50, konto.Bilans);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Wyplata_BrakSrodkow_RzucaWyjatek()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.Wyplata(150);
        }

        [TestMethod]
        public void BlokujKonto_UstawiaZablokowaneNaTrue()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.BlokujKonto();
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void OdblokujKonto_UstawiaZablokowaneNaFalse()
        {
            var konto = new Konto("Jan Kowalski", 100);
            konto.BlokujKonto();
            konto.OdblokujKonto();
            Assert.IsFalse(konto.Zablokowane);
        }
    }
}