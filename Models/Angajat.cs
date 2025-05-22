using System;

namespace MultiTab.Models
{
    public class Angajat
    {
        public string NumeUtilizator { get; set; }
        public string Parola { get; set; }
        public string TipAngajat { get; set; } // "Junior" sau "Senior"
        public string Email { get; set; }
        // Poți adăuga alte proprietăți relevante, cum ar fi data angajării, salariu etc.

        public Angajat(string numeUtilizator, string parola, string tipAngajat, string email)
        {
            NumeUtilizator = numeUtilizator;
            Parola = parola;
            TipAngajat = tipAngajat;
            Email = email;
        }

        // Constructor implicit pentru deserializare JSON
        public Angajat() { }
    }
}