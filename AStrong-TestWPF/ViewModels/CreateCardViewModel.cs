namespace AStrong_TestWPF.ViewModels
{
    using AStrong_TestWPF.Models;
    using BackendClientLib.Interfaces;
    using BackendClientLib.Services;
    using Microsoft.Win32;
    using Prism.Commands;
    using Prism.Mvvm;

    public class CreateCardViewModel : BindableBase
    {
        private readonly BackendClient backendClient;

        public CardModel CardModel { get; set; }

        public DelegateCommand ClickOpenFileDialog { get; private set; }

        public DelegateCommand ClickSave { get; private set; }

        public CreateCardViewModel()
        {
            this.backendClient = new BackendClient();

            this.CardModel = new CardModel();
            this.CardModel.PropertyChanged += (sender, args) => 
            {
                this.ClickSave?.RaiseCanExecuteChanged();
            };

            ClickSave = new DelegateCommand(ClickedSave, () =>
            {
                return !string.IsNullOrEmpty(this.CardModel.Description.Trim()) && !string.IsNullOrEmpty(this.CardModel.Image.Trim());
            });

            ClickOpenFileDialog = new DelegateCommand(ClickedOpenFileDialog);
        }

        private void ClickedOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG"
            };
            if (openFileDialog.ShowDialog() == true)
                CardModel.Image = openFileDialog.FileName;
        }

        private void ClickedSave()
        {
            Task.Run(() => this.backendClient.CreateCardAsync(this.CardModel.Description, this.CardModel.Image, CancellationToken.None)
                            .ContinueWith((res) => {  }));
        }
    }
}
