using System;

namespace Bank
{
    public class KontoLimit : IKonto
    {
        private Konto _konto;
        private decimal _limit;
        private bool _czyDebetWykorzystany = false;

        public KontoLimit(string klient, decimal bilansNaStart = 0, decimal limit = 100)
        {
            _konto = new Konto(klient, bilansNaStart);
            _limit = limit;
        }

        public KontoLimit(Konto istniejąceKonto, decimal limit = 100)
        {
            _konto = istniejąceKonto;
            _limit = limit;
        }

        public string Nazwa => _konto.Nazwa;
        public bool Zablokowane => _konto.Zablokowane;
        public decimal Limit { get => _limit; set => _limit = value; }
        public decimal Bilans => _konto.Bilans + _limit;

        public void Wplata(decimal kwota)
        {
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota));
            if (_konto.Zablokowane && _czyDebetWykorzystany)
            {
                _konto.WymusZmianeBilansu(kwota);
                if (_konto.Bilans >= 0)
                {
                    _czyDebetWykorzystany = false;
                    _konto.OdblokujKonto();
                }
            }
            else _konto.Wplata(kwota);
        }

        public void Wyplata(decimal kwota)
        {
            if (_konto.Zablokowane) throw new InvalidOperationException();
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota));

            if (kwota <= _konto.Bilans)
            {
                _konto.Wyplata(kwota);
            }
            else if (!_czyDebetWykorzystany && kwota <= _konto.Bilans + _limit)
            {
                _konto.WymusZmianeBilansu(-kwota);
                _czyDebetWykorzystany = true;
                _konto.BlokujKonto();
            }
            else throw new InvalidOperationException();
        }

        public void BlokujKonto() => _konto.BlokujKonto();
        public void OdblokujKonto() => _konto.OdblokujKonto();
    }
}