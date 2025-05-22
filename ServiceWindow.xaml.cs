using System.Windows;

namespace MultiTab
{
    public partial class ServiceWindow : Window
    {
        public ServiceWindow()
        {
            InitializeComponent();
        }

        private void TrimiteCerere_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cererea ta a fost trimisă cu succes. Vei fi contactat în curând!", "Service", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}