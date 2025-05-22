using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using MultiTab.Data; // Adaugă acest using
using MultiTab.Models; // Adaugă acest using

namespace MultiTab
{


    public partial class LoginWindow : Window
    {
        // Instanță a DataManager pentru a gestiona datele managerului
        private DataManager _dataManager;

        public LoginWindow()
        {
            InitializeComponent();
            _dataManager = new DataManager(); // Inițializează DataManager
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim(); // Folosim Trim() pentru a elimina spațiile
            string password = PasswordBox.Password.Trim(); // Folosim Trim() pentru a elimina spațiile

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Completează toate câmpurile!", "Avertisment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // PASUL 1: Încearcă autentificarea ca MANAGER
            Angajat manager = _dataManager.AutentificaManager(username, password);
            if (manager != null)
            {
                MessageBox.Show($"Autentificare reușită ca Manager: {manager.NumeUtilizator}", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ManagerWindow managerWindow = new ManagerWindow(_dataManager); // Trimitem instanța DataManager către ManagerWindow
                managerWindow.Show();
                this.Close();
                return; // Oprim execuția, managerul s-a autentificat
            }

            // PASUL 2: Continuă cu logica existentă pentru ADMIN și USER
            List<Utilizator> utilizatori = new List<Utilizator>();
            try
            {
                // Calea fișierului tău original, așa cum ai specificat-o
                string json = File.ReadAllText("C:\\Users\\Darius\\Desktop\\Proiect_IS-master\\ulizatori.json"); // Asigură-te că numele fișierului este corect (utilizatori.json, nu ulizatori.json)
                utilizatori = JsonConvert.DeserializeObject<List<Utilizator>>(json);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Fișierul 'ulizatori.json' nu a fost găsit la calea specificată.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la citirea fișierului utilizatori: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Caută utilizatorul în listă (pentru rolurile "admin" și "user")
            var utilizator = utilizatori.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (utilizator != null)
            {
                if (utilizator.Rol == "admin")
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else if (utilizator.Rol == "user")
                {
                    UserWindow userWindow = new UserWindow(); // Asigură-te că ai această fereastră
                    userWindow.Show();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Nume de utilizator sau parolă incorecte!", "Eroare de Autentificare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}