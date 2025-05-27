using entitati;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void add_prod_button_Click(object sender, RoutedEventArgs e)
        {
            InputProduse newInputElementeWindow = new InputProduse();
            newInputElementeWindow.ShowDialog();
        }

        private void afisare_prd_serv_Click(object sender, RoutedEventArgs e)
        {   

            text_box.Text = "";

            try {


                viewElem(typeof(Produs));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la afisarea elementelor: " + ex.Message);
            }

        }

        private void read_file_elem_Click(object sender, RoutedEventArgs e)
        {
            ProduseMgr produseMgr = new ProduseMgr();   
            produseMgr.CitireFisier();
            MessageBox.Show("Elemente citite din fisierul XML.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void add_serviciu_btn_Click(object sender, RoutedEventArgs e)
        {
            InputServiciu newInputServiciuWindow = new InputServiciu();
            newInputServiciuWindow.ShowDialog();
        }

        private void text_box_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void add_pachete_Click(object sender, RoutedEventArgs e)
        {
            InputPachete newInputPachete = new InputPachete();
            newInputPachete.ShowDialog();
        }

        private void afisare_pachete_Click(object sender, RoutedEventArgs e)
        {
            text_box.Text = "";

            try
            {


                viewElem(typeof(Pachet));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la afisarea elementelor: " + ex.Message);
            }
        }


        public void viewElem(Type type)
        {

            List<ProdusAbstract> elemente = ProduseMgrAbstract.Elemente;

            foreach (ProdusAbstract element in elemente)
            {
                if (type == element.GetType())
                {
                    text_box.Text += element.Descriere() + "\n";
                }
                else if (type == typeof(Produs) && (element is Produs || element is Serviciu) ||
                    type == typeof(Serviciu) && (element is Produs || element is Serviciu))
                {
                    text_box.Text += element.Descriere() + "\n";
                }


            }
        }

        private void serializare_XML_Click(object sender, RoutedEventArgs e)
        {
            ProduseMgr produseMgr = new ProduseMgr();
            produseMgr.save2XML("elemente_xml");
        }

        private void deserializare_XML_Click(object sender, RoutedEventArgs e)
        {
            ProduseMgr produseMgr = new ProduseMgr();
            produseMgr.loadFromXml("elemente_xml");
        }

        private void serializare_JSON_Click(object sender, RoutedEventArgs e)
        {
            ProduseMgr produseMgr = new ProduseMgr();
            produseMgr.save2JSON("elemente_json");
        }

        private void deserializare_JSON_Click(object sender, RoutedEventArgs e)
        {
            ProduseMgr produseMgr = new ProduseMgr();
            produseMgr.loadFromJson("elemente_json");
        }

        private void afisare_dupa_criterii_Click(object sender, RoutedEventArgs e)
        {
            AfisareCriteriuPret newAfisareDupaPret = new AfisareCriteriuPret();
            newAfisareDupaPret.ShowDialog();
        }

    }
}