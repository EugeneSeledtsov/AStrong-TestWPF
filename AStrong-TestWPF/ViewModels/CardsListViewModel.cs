namespace AStrong_TestWPF.ViewModels
{
    using AStrong_TestWPF.Models;
    using BackendClientLib.Services;
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.ObjectModel;

    public class CardsListViewModel : BindableBase
    {
        int skip = 0;
        readonly int take = 5;

        public ObservableCollection<CardModel> Cards { get; set; } = [];

        public DelegateCommand ClickNext { get; private set; }

        public DelegateCommand ClickPrev { get; private set; }

        public CardsListViewModel()
        {
            ClickNext = new DelegateCommand(ClickedNext, () =>
            {
                return Cards.Count == this.take;
            });

            ClickPrev = new DelegateCommand(ClickedPrev, () =>
            {
                return this.skip > 0;
            });

            this.FillModel();
        }

        private void FillModel()
        {
            var service = new BackendClient();

            var cardsList = service.GetCardsAsync(skip, take, CancellationToken.None)
                                .GetAwaiter().GetResult()
                                .Select(t => new CardModel() { Description = t.Description, Image = t.ImageUrl });
            Cards.Clear();
            Cards.AddRange(cardsList);
            this.ClickNext.RaiseCanExecuteChanged();
            this.ClickPrev.RaiseCanExecuteChanged();
        }

        private void ClickedNext()
        {
            skip += take;
            FillModel();
        }

        private void ClickedPrev()
        {
            skip -= take;
            FillModel();
        }
    }
}
