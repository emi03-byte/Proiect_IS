using MultiTab.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MultiTab
{
    public partial class AddProductWindow : Window
    {
        private readonly DataManager _dataManager;

        public AddProductWindow(DataManager dataManager)
        {
            InitializeComponent();
            _dataManager = dataManager;
        }

        private void BtnSalveaza_Click(object sender, RoutedEventArgs e)
        {
            // Validări de bază
            if (string.IsNullOrWhiteSpace(txtNume.Text) ||
                string.IsNullOrWhiteSpace(txtPret.Text) ||
                string.IsNullOrWhiteSpace(txtDescriere.Text) ||
                string.IsNullOrWhiteSpace(txtCategorie.Text))
            {
                MessageBox.Show("Completează toate câmpurile!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Parsează prețul
            if (!decimal.TryParse(txtPret.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal pret))
            {
                MessageBox.Show("Preț invalid!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Creează produsul și adaugă-l
            var produs = new Produs
            {
                Nume = txtNume.Text.Trim(),
                Pret = pret,
                Descriere = txtDescriere.Text.Trim(),
                Categorie = txtCategorie.Text.Trim()
            };

            if (!_dataManager.AdaugaProdus(produs))
            {
                MessageBox.Show("Produsul există deja sau nu a putut fi salvat.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Produs adăugat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}