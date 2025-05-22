using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace MultiTab
{
    public partial class MainWindow : Window
    {
        private List<Produs> produse = new List<Produs>();

        public MainWindow()
        {
            InitializeComponent();
            IncarcaProduseDinFisier();
        }

        private void IncarcaProduseDinFisier()
        {
            try
            {
                if (File.Exists("C:\\Users\\Darius\\Desktop\\Proiect_IS-master\\produse.json"))
                {
                    string json = File.ReadAllText("C:\\Users\\Darius\\Desktop\\Proiect_IS-master\\produse.json");
                    produse = JsonSerializer.Deserialize<List<Produs>>(json);
                    AfiseazaProduse(produse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la încărcarea produselor: " + ex.Message);
            }
        }

        private void AfiseazaProduse(List<Produs> lista)
        {
            ListaProduseMain.Items.Clear();
            foreach (var p in lista)
                ListaProduseMain.Items.Add($"{p.Nume} ({p.Categorie}) - {p.Pret} RON");
        }

        private void Button_LogIn(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void Button_CreareCont(object sender, RoutedEventArgs e)
        {
            CreareCont creareCont = new CreareCont();
            creareCont.ShowDialog();
        }

        private void Filtru_Click(object sender, RoutedEventArgs e)
        {
            string categorie = (sender as Button).Tag.ToString();
            if (categorie == "toate")
                AfiseazaProduse(produse);
            else
                AfiseazaProduse(produse.Where(p => p.Categorie.ToLower() == categorie.ToLower()).ToList());
        }
    }
}