using Prism.Commands;
using Xamarin.Forms;

namespace Demo.ViewModels.Interfaces
{

    public interface IMainPageViewModel
    {

        DelegateCommand FilePickCommand
        {
            get;
        }

        ImageSource SelectedImage
        {
            get;
        }

        string Title
        {
            get;
        }

    }

}