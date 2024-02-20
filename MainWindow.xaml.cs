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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module_12_WPF_HomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        Client<uint, string, string, string, uint> cl = new Client<uint, string, string, string, uint>();
        List<Client<uint, string, string, string, uint>> clients = new List<Client<uint, string, string, string, uint>>();
        DataBase data = new DataBase();
        BankAccount<uint, long, uint> ac = new BankAccount<uint, long, uint>();
   
        public MainWindow()
        {
            InitializeComponent();
            clients = data.GetClients();
            dataGrid.ItemsSource = clients;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cl = dataGrid.SelectedItem as Client<uint, string, string, string, uint>;
        }
        private void Button_ClickSearch(object sender, RoutedEventArgs e)
        {
             dataGrid.ItemsSource =  cl.SearchClient(txtSearch.Text);
        }
        private void ClickCreateAccount(object sender, RoutedEventArgs e)
        {         
            uint sum;
            bool isNumerical = uint.TryParse(txtSum.Text, out sum);

            if (isNumerical)
            {
                BankAccount<uint, long, uint> newAccount =  ac.CreateAccount(sum);

                cl.AddIdAccountClient(txtIdClient.Text, newAccount); 

                dataGrid.ItemsSource = cl.GetClients();
            }          
        }
        private void ClickAllBankAccounts(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = ac.GetAccounts();
        }
        private void ClickAllClients(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = cl.GetClients();
        }
        private void ClickDeleteAccount(object sender, RoutedEventArgs e)
        {
            ac.DeleteAccount(txtIdClient.Text);
            dataGrid.ItemsSource = cl.GetClients();
            dataGrid.ItemsSource = ac.GetAccounts();
        }

        private void Click_Transfer(object sender, RoutedEventArgs e)
        {
            ac.TransferMoney(txtNumDebit.Text, txtNumCredit.Text, txtSumTrans.Text);
            dataGrid.ItemsSource = ac.GetAccounts();
        }
    }
}
