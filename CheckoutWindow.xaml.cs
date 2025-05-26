using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace MultiTab
{
    public partial class CheckoutWindow : Window
    {
        public CheckoutWindow()
        {
            InitializeComponent();
            PlataComboBox.SelectionChanged += PlataComboBox_SelectionChanged;
            CardDetailsPanel.Visibility = Visibility.Collapsed; 
        }

        private void PlataComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((PlataComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() == "Card")
                CardDetailsPanel.Visibility = Visibility.Visible;
            else
                CardDetailsPanel.Visibility = Visibility.Collapsed;
        }

        private void PlaseazaComanda_Click(object sender, RoutedEventArgs e)
        {
            // Validări simple
            if (string.IsNullOrWhiteSpace(PrenumeTextBox.Text) ||
                string.IsNullOrWhiteSpace(NumeTextBox.Text) ||
                JudetComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(AdresaTextBox.Text) ||
                string.IsNullOrWhiteSpace(TelefonTextBox.Text))
            {
                MessageBox.Show("Te rugăm să completezi toate câmpurile personale.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(TelefonTextBox.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Numărul de telefon trebuie să aibă exact 10 cifre.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if ((PlataComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() == "Card")
            {
                if (string.IsNullOrWhiteSpace(CardNumberTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ExpirareTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CVVTextBox.Text))
                {
                    MessageBox.Show("Completează toate câmpurile pentru plata cu cardul.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(CardNumberTextBox.Text, @"^\d{16}$"))
                {
                    MessageBox.Show("Numărul cardului trebuie să aibă exact 16 cifre.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(ExpirareTextBox.Text, @"^(0[1-9]|1[0-2])\/\d{2}$"))
                {
                    MessageBox.Show("Data expirării trebuie să fie în format MM/YY.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(CVVTextBox.Text, @"^\d{3}$"))
                {
                    MessageBox.Show("CVV-ul trebuie să aibă exact 3 cifre.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            MessageBox.Show("Comanda a fost plasată cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
        }
    }
}
