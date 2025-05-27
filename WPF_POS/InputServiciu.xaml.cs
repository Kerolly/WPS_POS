using app1;
using entitati;
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

namespace WPF_POS
{
    /// <summary>
    /// Interaction logic for InputServiciu.xaml
    /// </summary>
    public partial class InputServiciu : Window
    {
        public InputServiciu()
        {
            InitializeComponent();
        }

        private void but_trimite_Click(object sender, RoutedEventArgs e)
        {
            string nume = input_nume_serv.Text;
            string codIntern = input_codIntern_serv.Text;
            int pret = int.Parse(input_pret_serv.Text);
            string categorie = input_categorie_serv.Text;

            ServiciiMgr serviciiMgr = new ServiciiMgr();
            serviciiMgr.AdaugareElemente(new Serviciu(serviciiMgr.IdGenerator(), nume, codIntern, pret, categorie));

            MessageBox.Show("Serviciul adaugat cu succes!");

            input_nume_serv.Text = "";
            input_codIntern_serv.Text = "";
            input_pret_serv.Text = "";
            input_categorie_serv.Text = "";
        }
    }
}
