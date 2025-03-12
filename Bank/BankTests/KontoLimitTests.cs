using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace BankTests
{
    [TestClass]
    public class KontoLimitTests
    {
        [TestMethod]
        public void Wyplata_ZUzyciemDebetu_BlokujeKonto()
        {
            var konto = new KontoLimit(klient: "Jan Kowalski", bilansNaStart: 100, limitDebetowy: 50);
            konto.Wyplata(kwota: 120);
            Assert.AreEqual(-20, konto.Bilans);
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void Wplata_PoDebecie_OdblokowujeKonto()
        {
            var konto = new KontoLimit(klient: "Jan Kowalski", bilansNaStart: 100, limitDebetowy: 50);
            konto.Wyplata(kwota: 120);
            konto.Wplata(kwota: 30);
            Assert.AreEqual(10, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
        }
    }
}


