using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Osveny> osvenyek = new List<Osveny>();
        string kivalasztottosveny = "osveny.txt";
        public MainWindow()
        {
            InitializeComponent();

        }
        List<string> mezok = new List<string>();
        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            string kivalasztottosveny = openFileDialog1.FileName;
            //MessageBox.Show(kivalasztottosveny);

            StreamReader sr = new StreamReader(kivalasztottosveny);
            while (!sr.EndOfStream)
            {
                String sor = sr.ReadLine();
                osvenyek.Add(new Osveny(sor));
                //MessageBox.Show($"Danko dibag: {sor}");
            }
            //lbJatek.ItemsSource = osvenyek;
          
            foreach (var os in osvenyek)
            {
                mezok.Add(os.MezoStringben());
            }

            if ((sender as Button).Tag == "1")
            {
                lbJatek.ItemsSource = mezok;
            }
            else
            {
                lbPalyaszerkeszto.ItemsSource = mezok;
            }

            
            //Jatek();
            //Jatek();
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            //Jatek game = new Jatek();
            //game.Mentes("yes.txt");
        }

        string szerkesztettsor = "";
        private void lbPalyaszerkeszto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int sor = lbPalyaszerkeszto.SelectedIndex;
            Osveny kivalasztott = osvenyek[sor];
            txtSzerkeszto.Text = kivalasztott.MezoStringben();
        }
        
        private void btnSormentes_Click(object sender, RoutedEventArgs e)
        {
            int szerkesztettsorindex = lbPalyaszerkeszto.SelectedIndex;

            szerkesztettsor = txtSzerkeszto.Text;
            osvenyek[szerkesztettsorindex].editMezo(szerkesztettsor);
            txtSzerkeszto.Clear();

            foreach (var os in osvenyek)
            {
                mezok.Add(os.MezoStringben());
            }

            lbPalyaszerkeszto.ItemsSource = mezok;
            
        }

        private void btnUjOsveny_Click(object sender, RoutedEventArgs e)
        {
            //Szia dani szia zsolti utalom a ciaganyokat ui: hugod jol nez ki?
            lbPalyaszerkeszto.Items.Add("");
        }
    }
}
