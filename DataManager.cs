using MultiTab.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiTab.Data
{
    public class DataManager
    {
        private static readonly string ManagerFilePath =
            "C:\\Users\\carun\\Downloads\\Proiect_IS-master\\Proiect_IS-master\\ManagerData.json";
        private static readonly string ProduseFilePath =
    "C:\\Users\\carun\\Downloads\\Proiect_IS-master\\Proiect_IS-master\\Produse.json";


        public List<Angajat> Angajati { get; set; }
        public List<Promotie> Promotii { get; set; }
        public List<Produs> Produse { get; set; }
        public DataManager()
        {
            Angajati = new List<Angajat>();
            Promotii = new List<Promotie>();
            Produse = new List<Produs>();
            IncarcaDateManager();
            IncarcaProduse();
        }

        private void IncarcaDateManager()
        {
            if (File.Exists(ManagerFilePath))
            {
                string json = File.ReadAllText(ManagerFilePath);
                var managerData = JsonConvert.DeserializeObject<ManagerDataContainer>(json);
                if (managerData != null)
                {
                    Angajati = managerData.Angajati ?? new List<Angajat>();
                    Promotii = managerData.Promotii ?? new List<Promotie>();
                    Produse = managerData.Produse ?? new List<Produs>();
                }
            }
        }

        public void SalveazaDateManager()
        {
            var managerData = new ManagerDataContainer
            {
                Angajati = Angajati,
                Promotii = Promotii,
                Produse = Produse
            };
            string json = JsonConvert.SerializeObject(managerData, Formatting.Indented);
            File.WriteAllText(ManagerFilePath, json);
        }

        public Angajat AutentificaManager(string numeUtilizator, string parola)
        {
            return Angajati
                .FirstOrDefault(a =>
                    a.NumeUtilizator == numeUtilizator &&
                    a.Parola == parola &&
                    a.TipAngajat == "Manager");
        }

        public Angajat AutentificaAngajat(string username, string parola)
        {
            try
            {
                // Dacă vrei să folosești lista deja încărcată:
                return Angajati.FirstOrDefault(a =>
                    a.NumeUtilizator == username &&
                    a.Parola == parola);

                // Sau, dacă vrei să citești direct din alt JSON:
                // string json = File.ReadAllText("angajati.json");
                // var angajati = JsonConvert.DeserializeObject<List<Angajat>>(json);
                // return angajati.FirstOrDefault(a =>
                //     a.NumeUtilizator == username && a.Parola == parola);
            }
            catch
            {
                return null;
            }
        }

        public bool AdaugaAngajat(Angajat angajat)
        {
            if (Angajati.Any(a => a.NumeUtilizator == angajat.NumeUtilizator))
                return false;

            Angajati.Add(angajat);
            SalveazaDateManager();
            return true;
        }

        public bool StergeAngajat(string numeUtilizator)
        {
            var angajatDeSters = Angajati.FirstOrDefault(a => a.NumeUtilizator == numeUtilizator);
            if (angajatDeSters != null)
            {
                Angajati.Remove(angajatDeSters);
                SalveazaDateManager();
                return true;
            }
            return false;
        }

        public bool AdaugaPromotie(Promotie promotie)
        {
            if (Promotii.Any(p => p.NumePromotie == promotie.NumePromotie))
                return false;

            Promotii.Add(promotie);
            SalveazaDateManager();
            return true;
        }

        public bool StergePromotie(string numePromotie)
        {
            var promotieDeSters = Promotii.FirstOrDefault(p => p.NumePromotie == numePromotie);
            if (promotieDeSters != null)
            {
                Promotii.Remove(promotieDeSters);
                SalveazaDateManager();
                return true;
            }
            return false;
        }
        public bool AdaugaProdus(Produs produs)
        {
            if (Produse.Any(p => p.Nume.Equals(produs.Nume, StringComparison.OrdinalIgnoreCase)))
                return false;

            Produse.Add(produs);
            SalveazaProduse();  // <- în loc de SalveazaDateManager()
            return true;
        }

        private void IncarcaProduse()
        {
            if (File.Exists(ProduseFilePath))
            {
                string json = File.ReadAllText(ProduseFilePath);
                Produse = JsonConvert.DeserializeObject<List<Produs>>(json) ?? new List<Produs>();
            }
            else
            {
                Produse = new List<Produs>();
            }
        }

        private void SalveazaProduse()
        {
            string json = JsonConvert.SerializeObject(Produse, Formatting.Indented);
            File.WriteAllText(ProduseFilePath, json);
        }

    }


    public class ManagerDataContainer
    {
        public List<Angajat> Angajati { get; set; }
        public List<Promotie> Promotii { get; set; }
        public List<Produs> Produse { get; set; }
    }
}
