using System;

namespace Bank
{
    public class Konto : IKonto
    {
        private string klient;
        protected decimal bilans;
        protected bool zablokowane = false;

        private Konto() { }

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            if (string.IsNullOrWhiteSpace(klient)) throw new ArgumentException();
            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        public Konto(IKonto inneKonto)
        {
            this.klient = inneKonto.Nazwa;
            this.bilans = inneKonto.Bilans;
            this.zablokowane = inneKonto.Zablokowane;
        }

        public string Nazwa => klient;
        public virtual decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        public virtual void Wplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException();
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota));
            bilans += kwota;
        }

        public virtual void Wyplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException();
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota));
            if (kwota > bilans) throw new InvalidOperationException();
            bilans -= kwota;
        }

        internal void WymusZmianeBilansu(decimal kwota)
        {
            bilans += kwota;
        }

        public void BlokujKonto() => zablokowane = true;
        public void OdblokujKonto() => zablokowane = false;
    }
}