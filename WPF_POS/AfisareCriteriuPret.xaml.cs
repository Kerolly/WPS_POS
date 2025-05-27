using System;
using System.Collections.Generic;
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
using System.Xml.Linq;
using entitati;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for AfisareCriteriuPret.xaml
    /// </summary>
    public partial class AfisareCriteriuPret : Window
    {
        public AfisareCriteriuPret()
        {
            InitializeComponent();
        }

        private void btn_afisare_Click(object sender, RoutedEventArgs e)
        {
            output_rez.Text = "";
            try
            {
                List<ProdusAbstract> elemente = ProduseMgrAbstract.Elemente;

                var inputCriteriu = input_criteriu_pret.Text;

                var filtru = new FiltrareCriteriu();

                if (int.TryParse(inputCriteriu, out int criteriuPret))
                {
                    var criteriu = new CriteriuPret(criteriuPret);
                    var elementeFiltrate = filtru.Filtrare(elemente, criteriu);

                    viewElemFiltrare(elementeFiltrate);
                }
                else
                {
                    var criteriu = new CriteriuCategorie(inputCriteriu);
                    var elementeFiltrare = filtru.Filtrare(elemente, criteriu);

                    viewElemFiltrare(elementeFiltrare);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la filtrare: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void viewElemFiltrare(IEnumerable<ProdusAbstract> elementeFiltrate)
        {
            try
            {
                if (elementeFiltrate.Count() == 0)
                {
                    MessageBox.Show("Nu exista elemente dupa acest criteriu!\nAsigurate ca ai citit elemente!");
                }
                else
                {

                    MessageBox.Show("\nFiltrare reusita!");

                    foreach (var elem in elementeFiltrate)
                    {
                        output_rez.Text += elem.Descriere() + "\n";
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la afisare: " + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }

    
}
