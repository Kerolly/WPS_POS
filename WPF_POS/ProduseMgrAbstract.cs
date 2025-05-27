using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using entitati;
using WPF_POS;

namespace entitati
{
    public abstract class ProduseMgrAbstract
    {
        protected static List<ProdusAbstract> elemente = new List<ProdusAbstract>();
        protected static int Id = 0;

        public static List<ProdusAbstract> Elemente => elemente;

        public uint IdGenerator()
        {
            return (uint)Interlocked.Increment(ref Id);
        }

        public void AdaugareElemente(ProdusAbstract element)
        {
            elemente.Add(element);
        }

        public virtual void WriteElemente()
        {
            // afisam serviciile
            MessageBox.Show("\n-------------------");
            MessageBox.Show("Serviciile sunt:");
        }


        public void Write2Box(Type type)
        {

            foreach (ProdusAbstract element in elemente)
            {
                if (type == element.GetType())
                {
                    MessageBox.Show("\n-------------------");
                    MessageBox.Show(element.Descriere());
                }
                else if (type == typeof(Produs) && (element is Produs || element is Serviciu) ||
                    type == typeof(Serviciu) && (element is Produs || element is Serviciu))
                {
                    MessageBox.Show("\n-------------------");
                    MessageBox.Show(element.Descriere());
                }


            }
        }


        public bool VerificareElemente(ProdusAbstract elemNou)
        {

            foreach (ProdusAbstract element in elemente)
            {
                if (element.Equals(elemNou))
                    return false;
            }

            return true;
        }

        public void InitListaFromXml()
        {

            try
            {
                //initializare fisier xml
                XmlDocument xmlDoc = new XmlDocument();

                //incarcare fisier
                xmlDoc.Load("D:\\Andrei Facultate\\Programare Orientata pe Obiecte\\Laborator\\Proiect\\WPF_POS\\WPF_POS\\Elemente.txt");

                //selectare noduri
                XmlNodeList lista_noduri_produse = xmlDoc.SelectNodes("/elemente/Produs");
                XmlNodeList lista_noduri_servicii = xmlDoc.SelectNodes("/elemente/Serviciu");

                XmlNodeList lista_noduri_pachete = xmlDoc.SelectNodes("/elemente/Pachet");
                //XmlNodeList lista_noduri_produse_pachet = xmlDoc.SelectNodes("/elemente/Pachet/Produs");
                //XmlNodeList lista_noduri_servicii_pachet = xmlDoc.SelectNodes("/elemente/Pachet/Serviciu");


                //Produsele
                foreach (XmlNode nod in lista_noduri_produse)
                {
                    string nume = nod["Nume"].InnerText;
                    string codIntern = nod["CodIntern"].InnerText;
                    string producator = nod["Producator"].InnerText;
                    int pret = int.Parse(nod["Pret"].InnerText);
                    string categorie = nod["Categorie"].InnerText;

                    //adaugare in lista
                    elemente.Add(new Produs(IdGenerator(), nume, codIntern, pret, categorie, producator));
                }


                //Serviciile
                foreach (XmlNode nod in lista_noduri_servicii)
                {
                    string nume = nod["Nume"].InnerText;
                    string codIntern = nod["CodIntern"].InnerText;
                    int pret = int.Parse(nod["Pret"].InnerText);
                    string categorie = nod["Categorie"].InnerText;

                    //adaugare in lista
                    elemente.Add(new Serviciu((uint)elemente.Count + 1, nume, codIntern, pret, categorie));
                }

                //Pachetele
                foreach (XmlNode nodPachet in lista_noduri_pachete)
                {
                    string numePachet = nodPachet["Nume"].InnerText;
                    string codInternPachet = nodPachet["CodIntern"].InnerText;
                    string categoriePachet = nodPachet["Categorie"].InnerText;

                    int pretTotalPachet = 0;

                    Pachet pachetNou = new Pachet(IdGenerator(), numePachet, codInternPachet, 0, categoriePachet);

                    XmlNodeList produsePachet = nodPachet.SelectNodes("Produs");
                    XmlNodeList serviciiPachet = nodPachet.SelectNodes("Serviciu");

                    foreach (XmlNode nodProdus in produsePachet)
                    {

                        string numeProdusPachet = nodProdus["Nume"].InnerText;
                        string codInternProdusPachet = nodProdus["CodIntern"].InnerText;
                        int pretProdusPachet = int.Parse(nodProdus["Pret"].InnerText);
                        string categorieProdusPachet = nodProdus["Categorie"].InnerText;
                        string producatorProdusPachet = nodProdus["Producator"].InnerText;



                        Produs produsNou = new Produs(
                            IdGenerator(),
                            numeProdusPachet,
                            codInternProdusPachet,
                            pretProdusPachet,
                            categorieProdusPachet,
                            producatorProdusPachet);


                        if (produsNou.canAddToPackage(pachetNou))
                        {
                            pachetNou.AddElement(produsNou);
                            pretTotalPachet += pretProdusPachet;
                        }
                        else
                            break;


                    }

                    foreach (XmlNode nodServiciu in serviciiPachet)
                    {
                        string numeServiciuPachet = nodServiciu["Nume"].InnerText;
                        string codInternServiciuPachet = nodServiciu["CodIntern"].InnerText;
                        int pretServiciuPachet = int.Parse(nodServiciu["Pret"].InnerText);
                        string categorieServiciuPachet = nodServiciu["Categorie"].InnerText;


                        Serviciu serviciuNou = new Serviciu(
                            IdGenerator(),
                            numeServiciuPachet,
                            codInternServiciuPachet,
                            pretServiciuPachet,
                            categorieServiciuPachet);

                        if (serviciuNou.canAddToPackage(pachetNou))
                        {
                            pachetNou.AddElement(serviciuNou);
                            pretTotalPachet += pretServiciuPachet;
                        }
                        else
                            break;

                    }


                    pachetNou.Pret = pretTotalPachet;
                    elemente.Add(pachetNou);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la citirea fisierului XML: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

            public abstract void CitireFisier();


        //Serializare 
        public void save2XML(string fileName)
        {
            Type[] prodAbstractTypes = new Type[3];
            prodAbstractTypes[0] = typeof(Serviciu);
            prodAbstractTypes[1] = typeof(Produs);
            prodAbstractTypes[2] = typeof(Pachet);

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<ProdusAbstract>), prodAbstractTypes);
                StreamWriter writer = new StreamWriter(fileName + ".xml");
                xs.Serialize(writer, elemente);
                writer.Close();
                MessageBox.Show("Salvat cu succes!");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Eroare la serializare:\n" + ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Eroare la scrierea fisierului:\n" + ex.Message);

            }
        }


        //deserializare
        public ProdusAbstract? loadFromXml(string fileName)
        {

            Type[] prodAbstractTypes = new Type[3];
            prodAbstractTypes[0] = typeof(Serviciu);
            prodAbstractTypes[1] = typeof(Produs);
            prodAbstractTypes[2] = typeof(Pachet);

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<ProdusAbstract>), prodAbstractTypes);

                FileStream fs = new FileStream(fileName + ".xml", FileMode.Open);
                XmlReader reader = new XmlTextReader(fs);

                var loadedElem = (List<ProdusAbstract>?)xs.Deserialize(reader);
                elemente.AddRange(loadedElem);
                fs.Close();
                MessageBox.Show("Deserializat cu succes!");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Eroare la deserializare:\n" + ex.Message);
            }
            catch (IOException ex)
            {
                MessageBox.Show("Eroare la citirea fisierului:\n" + ex.Message);
            }

            return null;
        }


        //serializare JSON
        public void save2JSON(string fileName)
        {

            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };


                string Json = JsonSerializer.Serialize(elemente, options);
                File.WriteAllText(fileName + ".json", Json);

                MessageBox.Show("Salvat cu succes!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la serializare JSON:\n" + ex.Message);
            }
        }


        //deserializare JSON
        public void loadFromJson(string fileName)
        {
            try
            {
                string jsonContent = File.ReadAllText(fileName + ".json");

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver(), //polymorphic deser
                    PropertyNameCaseInsensitive = true
                };

                var loadedElem = JsonSerializer.Deserialize<List<ProdusAbstract>>(jsonContent);

                elemente.AddRange(loadedElem);

                MessageBox.Show("Deserializat cu succes!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la deserializare JSON:\n" + ex.Message);
            }
        }


        //filtrare elemente
        public void Filtru(ICriteriu criteriu)
        {
            var filtru = new FiltrareCriteriu();

            var elementeFiltrate = filtru.Filtrare(elemente, criteriu);

            if (elementeFiltrate.Count() == 0)
            {
               MessageBox.Show("Nu exista elemente dupa acest criteriu!");
            }
            else
            {

                MessageBox.Show("\nRezultate filtrare: ");
                
                foreach (var elem in elementeFiltrate)
                {
                    MessageBox.Show(elem.Descriere());
                }

            }
        }


    }

        
}

