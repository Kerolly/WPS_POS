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
    /// Interaction logic for AdaguareElem.xaml
    /// </summary>
    public partial class AdaguareElem : Window
    {
        private Pachet? _pachetNou; //variabila la nivel de clasa
        private PacheteMgr _pacheteMgr = new PacheteMgr(); //instanta de manager pentru pachete la nivel de clasa
        private int tipElement; // 1 = produs, 0 = serviciu

        public AdaguareElem()
        {
            InitializeComponent();
        }

        public AdaguareElem(Pachet pachetNou, int tip)
        {
            InitializeComponent();
            this._pachetNou = pachetNou;
            this.tipElement = tip;

            if (tipElement == 0) { 
            
                input_producator_elem.Visibility = Visibility.Collapsed; //ascunde inputul pentru producator
                producator_text.Visibility = Visibility.Collapsed;
            }

        }

        private void add_prod_btn_Click(object sender, RoutedEventArgs e)
        {

            string nume = input_nume_elem.Text;
            string codIntern = input_codIntern_elem.Text;
            int pret = int.Parse(input_pret_elem.Text);
            string categorie = input_categorie_elem.Text;

            if (tipElement == 1)
            {

                string producator = input_producator_elem.Text;


                ProdusAbstract produsNou = new Produs(_pacheteMgr.IdGenerator(), nume, codIntern, pret, categorie, producator);

                if (produsNou is ProdusAbstract prodAbstr &&
                    prodAbstr.canAddToPackage(_pachetNou))
                {
                    _pachetNou.AddElement(produsNou);
                    _pachetNou.Pret += pret; //adaug pretul produsului la pretul total al pachetului

                    MessageBox.Show("Produsul a fost adaugat cu succes in pachet!");

                    input_nume_elem.Text = "";
                    input_codIntern_elem.Text = "";
                    input_pret_elem.Text = "";
                    input_categorie_elem.Text = "";
                    input_producator_elem.Text = "";

                    this.Close(); //inchide fereastra dupa adaugare

                }
                else
                {
                    MessageBox.Show("Nu se poate adauga elementul in pachet! Max 2");
                }

            }
            else
            {
                ProdusAbstract serviciuNou = new Serviciu(_pacheteMgr.IdGenerator(), nume, codIntern, pret, categorie);


                if (serviciuNou is ProdusAbstract prodAbstr &&
                    prodAbstr.canAddToPackage(_pachetNou))
                {
                    _pachetNou.AddElement(serviciuNou);
                    _pachetNou.Pret += pret; //adaug pretul serviciului la pretul total al pachetului

                    MessageBox.Show("Serviciul a fost adaugat cu succes in pachet!");

                    input_nume_elem.Text = "";
                    input_codIntern_elem.Text = "";
                    input_pret_elem.Text = "";
                    input_categorie_elem.Text = "";
                    input_producator_elem.Text = "";

                    this.Close(); //inchide fereastra dupa adaugare

                }
                else
                {
                    MessageBox.Show("Nu se poate adauga elementul in pachet! Max 2");
                }

            }

        }
    }
}
