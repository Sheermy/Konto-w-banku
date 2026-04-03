using System;

namespace Bank
{
    public class KontoPlus : Konto
    {
        private decimal limit;
        private bool czyDebetWykorzystany = false;

        public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limit = 100)
            : base(klient, bilansNaStart)
        {
            this.limit = limit;
        }

        public KontoPlus(Konto inneKonto, decimal limit = 100) : base(inneKonto)
        {
            this.limit = limit;
        }

        public decimal Limit
        {
            get => limit;
            set => limit = value;
        }

        public override decimal Bilans => bilans + limit;

        public override void Wyplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException();
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota));

            if (kwota <= bilans)
            {
                base.Wyplata(kwota);
            }
            else if (!czyDebetWykorzystany && kwota <= bilans + limit)
            {
                bilans -= kwota;
                czyDebetWykorzystany = true;
                BlokujKonto();
            }
            else throw new InvalidOperationException();
        }

        public override void Wplata(decimal kwota)
        {
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota));

            if (zablokowane && czyDebetWykorzystany)
            {
                bilans += kwota;
                if (bilans >= 0)
                {
                    czyDebetWykorzystany = false;
                    OdblokujKonto();
                }
            }
            else base.Wplata(kwota);
        }
    }
}