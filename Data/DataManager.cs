using MultiTab.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiTab.Data
{
    public class DataManager
    {
        private static readonly string ManagerFilePath = "C:\\Users\\Florian\\Desktop\\Proiect_IS-master\\ManagerData.json";

        public List<Angajat> Angajati { get; set; }
        public List<Promotie> Promotii { get; set; }

        public DataManager()
        {
            Angajati = new List<Angajat>();
            Promotii = new List<Promotie>();
            IncarcaDateManager();
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
                }
            }
        }

        public void SalveazaDateManager()
        {
            var managerData = new ManagerDataContainer
            {
                Angajati = Angajati,
                Promotii = Promotii
            };
            string json = JsonConvert.SerializeObject(managerData, Formatting.Indented);
            File.WriteAllText(ManagerFilePath, json);
        }

        public Angajat AutentificaManager(string numeUtilizator, string parola)
        {
            return Angajati.FirstOrDefault(a => a.NumeUtilizator == numeUtilizator && a.Parola == parola && a.TipAngajat == "Manager");
        }

    
        public bool AdaugaAngajat(Angajat angajat)
        {
            if (Angajati.Any(a => a.NumeUtilizator == angajat.NumeUtilizator))
            {
                return false; 
            }
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
            {
                return false; 
            }
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
    }
    public class ManagerDataContainer
    {
        public List<Angajat> Angajati { get; set; }
        public List<Promotie> Promotii { get; set; }
    }
}
