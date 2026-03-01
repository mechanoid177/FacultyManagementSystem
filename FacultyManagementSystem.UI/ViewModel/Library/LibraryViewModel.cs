using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FacultyManagementSystem.Library;
using System.Collections.ObjectModel;
using FacultyManagementSystem.Library.Interfaces;
using FacultyManagementSystem.Utility;

namespace FacultyManagementSystem.UI.ViewModel
{
    public partial class LibraryViewModel : ObservableObject, IDisposable
    {
        private ILibrary _library;

        

        

        public event EventHandler<MessengerEventArgs> MessageReceived;

        public LibraryViewModel(ILibrary library)
        {
            _library = library;

            _library.ActionFailed += (s, e) => OnMessageReceived(e.Message);
        }

        

        protected void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, new MessengerEventArgs(message));
        }

        public void Dispose()
        {
            _library?.Dispose();
        }
    }
}