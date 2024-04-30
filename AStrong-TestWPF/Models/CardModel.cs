namespace AStrong_TestWPF.Models
{
    using Prism.Mvvm;

    public class CardModel : BindableBase
    {
        private string description = string.Empty;
        private string image = string.Empty;

        public string Description {
            get { return description; } 
            set { SetProperty(ref description, value); }
        }

        public string Image {
            get { return image; } 
            set { SetProperty(ref image, value); }
        }
    }
}
