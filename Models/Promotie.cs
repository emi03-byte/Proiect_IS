using System;
using System.Collections.Generic;

namespace MultiTab.Models
{
    public class Promotie
    {
        public string NumePromotie { get; set; }
        public string Descriere { get; set; }
        public List<string> ArticoleIncluse { get; set; } // O listă de ID-uri sau Nume de produse/piese
        public DateTime DataStart { get; set; }
        public DateTime DataSfarsit { get; set; }
        public double DiscountProcent { get; set; } // 10% conform cerintei

        public Promotie(string numePromotie, string descriere, List<string> articoleIncluse, DateTime dataStart, DateTime dataSfarsit, double discountProcent)
        {
            NumePromotie = numePromotie;
            Descriere = descriere;
            ArticoleIncluse = articoleIncluse;
            DataStart = dataStart;
            DataSfarsit = dataSfarsit;
            DiscountProcent = discountProcent;
        }

        // Constructor implicit pentru deserializare JSON
        public Promotie() { }
    }
}