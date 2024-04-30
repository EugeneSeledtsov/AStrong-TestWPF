using AStrong_TestWPF.ViewModels;
using System.Windows;

namespace AStrong_TestWPF
{
    /// <summary>
    /// Логика взаимодействия для CardsList.xaml
    /// </summary>
    public partial class CardsList : Window
    {
        public CardsList()
        {
            InitializeComponent();

            DataContext = new CardsListViewModel();
        }

        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateCard();
            createWindow.Show();
            this.Close();
        }
    }
}
