namespace Bank;

public class Konto
{
    protected string klient; 
    protected decimal bilans; 
    protected bool zablokowane = false;

    public Konto(string klient, decimal bilansNaStart = 0)
    {
        if (string.IsNullOrWhiteSpace(klient))
            throw new ArgumentException("Nazwa klienta nie może być pusta.");
        if (bilansNaStart < 0)
            throw new ArgumentException("Początkowy bilans nie może być ujemny.");

        this.klient = klient;
        this.bilans = bilansNaStart;
    }

    public string Nazwa => klient;
    public decimal Bilans => bilans;
    public bool Zablokowane => zablokowane;

    public virtual void Wplata(decimal kwota)
    {
        if (zablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0)
            throw new ArgumentException("Kwota wpłaty musi być większa od zera.");

        bilans += kwota;
    }

    public virtual void Wyplata(decimal kwota)
    {
        if (zablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0)
            throw new ArgumentException("Kwota wypłaty musi być większa od zera.");
        if (kwota > bilans)
            throw new InvalidOperationException("Niewystarczające środki na koncie.");

        bilans -= kwota;
    }

    public void BlokujKonto()
    {
        zablokowane = true;
    }

    public void OdblokujKonto()
    {
        zablokowane = false;
    }
}