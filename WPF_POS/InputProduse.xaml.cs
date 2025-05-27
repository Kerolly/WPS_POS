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
    /// Interaction logic for InputProduse.xaml
    /// </summary>
    public partial class InputProduse : Window
    {
        public InputProduse()
        {
            InitializeComponent();
        }

        private void but_trimite_Click(object sender, RoutedEventArgs e)
        {   
            string nume = input_nume_prod.Text;
            string codIntern = input_codIntern_prod.Text;
            int pret = int.Parse(input_pret_prod.Text);
            string producator = input_producator_prod.Text;
            string categorie = input_categorie_prod.Text;

            ProduseMgr produseMgr = new ProduseMgr();
            produseMgr.AdaugareElemente(new Produs(produseMgr.IdGenerator(), nume, codIntern, pret, categorie, producator));

            MessageBox.Show("Produsul adaugat cu succes!");

            input_nume_prod.Text = "";
            input_codIntern_prod.Text = "";
            input_pret_prod.Text = "";
            input_producator_prod.Text = "";
            input_categorie_prod.Text = "";
        }
    }
}
