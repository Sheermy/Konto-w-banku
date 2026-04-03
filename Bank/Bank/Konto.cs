using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class Konto
    {
        private string klient;
        protected decimal bilans; // protected, by klasy pochodne widziały stan
        protected bool zablokowane = false;

        // Konstruktor domyślny prywatny - blokuje tworzenie pustych kont
        private Konto() { }

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            if (string.IsNullOrWhiteSpace(klient)) throw new ArgumentException("Nazwa klienta nie może być pusta.");
            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        public string Nazwa => klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        public virtual void Wplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException("Konto zablokowane.");
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota), "Kwota musi być dodatnia.");
            bilans += kwota;
        }

        public virtual void Wyplata(decimal kwota)
        {
            if (zablokowane) throw new InvalidOperationException("Konto zablokowane.");
            if (kwota <= 0) throw new ArgumentOutOfRangeException(nameof(kwota), "Kwota musi być dodatnia.");
            if (kwota > bilans) throw new InvalidOperationException("Brak środków.");
            bilans -= kwota;
        }

        public void BlokujKonto() => zablokowane = true;
        public void OdblokujKonto() => zablokowane = false;
    }
}
