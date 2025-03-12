namespace Bank;

public class KontoPlus : Konto
{
    private decimal limitDebetowy;
    private bool uzytoDebetu;

    public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limitDebetowy = 0) 
        : base(klient, bilansNaStart)
    {
        if (limitDebetowy < 0)
            throw new ArgumentException("Limit debetowy nie może być ujemny.");
        this.limitDebetowy = limitDebetowy;
        this.uzytoDebetu = false;
    }

    public decimal LimitDebetowy
    {
        get => limitDebetowy;
        set
        {
            if (value < 0)
                throw new ArgumentException("Limit debetowy nie może być ujemny.");
            limitDebetowy = value;
        }
    }

    public new decimal Bilans => base.Bilans;

    public override void Wplata(decimal kwota)
    {
        if (kwota <= 0)
            throw new ArgumentException("Kwota wpłaty musi być większa od zera.");

        bilans += kwota;

        if (uzytoDebetu && bilans >= 0)
        {
            uzytoDebetu = false;
            OdblokujKonto();
        }
    }

    public override void Wyplata(decimal kwota)
    {
        if (Zablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");
        if (kwota <= 0)
            throw new ArgumentException("Kwota wypłaty musi być większa od zera.");

        decimal dostepneSrodki = base.Bilans + (uzytoDebetu ? 0 : limitDebetowy);
        if (kwota > dostepneSrodki)
            throw new InvalidOperationException("Niewystarczające środki, даже z limitem debetowym.");

        bilans -= kwota;

        if (bilans < 0 && !uzytoDebetu)
        {
            uzytoDebetu = true;
            BlokujKonto();
        }
    }
}