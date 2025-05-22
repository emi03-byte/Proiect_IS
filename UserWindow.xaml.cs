using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace MultiTab
{
    public partial class UserWindow : Window
    {
        private List<Produs> produse = new List<Produs>();
        private List<Produs> cosCumparaturi = new List<Produs>();

        public UserWindow()
        {
            InitializeComponent();
            IncarcaProduse();
        }

        private void IncarcaProduse()
        {
            try
            {
                string caleFisier = "C:\\Users\\Darius\\Desktop\\Proiect_IS-master\\produse.json";
                if (File.Exists(caleFisier))
                {
                    string json = File.ReadAllText(caleFisier);
                    produse = JsonSerializer.Deserialize<List<Produs>>(json);
                    ListaProduseUser.ItemsSource = produse;
                }
                else
                {
                    MessageBox.Show("Fișierul produse.json nu a fost găsit.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea catalogului: " + ex.Message);
            }
        }

        private void DeschideService_Click(object sender, RoutedEventArgs e)
        {
            ServiceWindow fereastraService = new ServiceWindow();
            fereastraService.ShowDialog();
        }
        private void Filtru_Click(object sender, RoutedEventArgs e)
        {
            string categorie = (sender as Button)?.Tag.ToString();
            if (categorie == "toate")
            {
                ListaProduseUser.ItemsSource = produse;
            }
            else
            {
                var filtrate = produse.Where(p => p.Categorie?.ToLower() == categorie.ToLower()).ToList();
                ListaProduseUser.ItemsSource = filtrate;
            }
        }

        private void AdaugaInCos_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var produs = (button.DataContext as Produs);
                if (produs != null)
                {
                    cosCumparaturi.Add(produs);
                    MessageBox.Show($"Produsul \"{produs.Nume}\" a fost adăugat în coș.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void VeziCos_Click(object sender, RoutedEventArgs e)
        {
            CosWindow cosWindow = new CosWindow(cosCumparaturi);
            cosWindow.ShowDialog();
        }
    }
}