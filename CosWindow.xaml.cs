using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MultiTab
{
    public partial class CosWindow : Window
    {
        public CosWindow(List<Produs> produse)
        {
            InitializeComponent();
            ListaCos.ItemsSource = produse;
            double total = (double)produse.Sum(p => p.Pret);
            TextTotal.Text = $"Total: {total} RON";
        }
    }
}