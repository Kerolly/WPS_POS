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
using entitati;

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for InputPachete.xaml
    /// </summary>
    /// 


    public partial class InputPachete : Window
    {

        private Pachet _pachetNou = new Pachet(); //variabila la nivel de clasa
        private PacheteMgr _pacheteMgr = new PacheteMgr(); //instanta de manager pentru pachete

        public InputPachete()
        {
            InitializeComponent();
        }

        private void but_trimite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nume = input_nume_pachet.Text;
                string codIntern = input_codIntern_pachet.Text;
                int pret = 0;
                string categorie = input_categorie_pachet.Text;
                int nr_prod = int.Parse(input_nr_prod_pachet.Text);
                int nr_serv = int.Parse(input_nr_serv_pachet.Text);

                _pachetNou = new Pachet(_pacheteMgr.IdGenerator(), nume, codIntern, pret, categorie);

                for(int i = 0; i < nr_prod; i++)
                {
                    var newInputProdus = new AdaguareElem(_pachetNou, 1);
                    newInputProdus.ShowDialog();
                }

                for (int i = 0; i < nr_serv; i++)
                {
                    var newInputProdus = new AdaguareElem(_pachetNou, 0);
                    newInputProdus.ShowDialog();
                }



                _pacheteMgr.AdaugareElemente(_pachetNou);

                input_nume_pachet.Text = "";
                input_codIntern_pachet.Text = "";
                input_categorie_pachet.Text = "";
                input_nr_prod_pachet.Text = "";
                input_nr_serv_pachet.Text = "";


                MessageBox.Show("Pachetul a fost adaugat cu succes!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Eroare la adaugarea pachetului. Verifica datele introduse." + ex.Message, "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
