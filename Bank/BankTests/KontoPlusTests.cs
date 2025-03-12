using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace BankTests
{
    [TestClass]
    public class KontoPlusTests
    {
        [TestMethod]
        public void Wyplata_ZUzyciemDebetu_BlokujeKonto()
        {
            var konto = new KontoPlus("Jan Kowalski", 100, 50);
            konto.Wyplata(120);
            Assert.AreEqual(-20, konto.Bilans);
            Assert.IsTrue(konto.Zablokowane);
        }

        [TestMethod]
        public void Wplata_PoDebecie_OdblokowujeKonto()
        {
            var konto = new KontoPlus("Jan Kowalski", 100, 50);
            konto.Wyplata(120); 
            konto.Wplata(30);  
            Assert.AreEqual(10, konto.Bilans); 
            Assert.IsFalse(konto.Zablokowane); 
        }
    }
}