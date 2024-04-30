namespace AStrong_TestWPF
{
    using AStrong_TestWPF.ViewModels;
    using System.Windows;

    /// <summary>
    /// Логика взаимодействия для CreateCard.xaml
    /// </summary>
    public partial class CreateCard : Window
    {
        public CreateCard()
        {
            InitializeComponent();
            DataContext = new CreateCardViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new CardsList();
            newWindow.Show();
            this.Close();
        }
    }
}
