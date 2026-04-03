using System;

namespace Bank
{
    public class Konto
    {
        private string klient;
        private decimal bilans;
        private bool zablokowane = false;

        private Konto() { }

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            if (string.IsNullOrWhiteSpace(klient)) throw new ArgumentException();
            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        public string Nazwa => klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        public void Wplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException();
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota));
            bilans += kwota;
        }

        public void Wyplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException();
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota));
            if (kwota > bilans) throw new InvalidOperationException();
            bilans -= kwota;
        }

        public void BlokujKonto() => zablokowane = true;
        public void OdblokujKonto() => zablokowane = false;
    }
}