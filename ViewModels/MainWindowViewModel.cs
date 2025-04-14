
using System.Windows.Input;
using System.Windows;
using JwkTool2.Models;
using JwkTool2.Models.Jwk;
using Microsoft.Win32;

namespace JwkTool2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _selectedFilePath = "";
        private string _publicKey = "";
        private string _fileLabel = "";
        private string _kid = "";
        private string _jwk = "";
        private string _rsaKey = "";

        private readonly IJwkCreator _jwkCreator;
        private readonly IPubKeyHandler _pubKeyHandler;

        public MainWindowViewModel(IJwkCreator jwkCreator, IPubKeyHandler pubKeyHandler, AppConfig config)
        {
            SelectFileCommand = new CommandBase(ExecuteSelectFileCommand);
            CreateJwkFileCommand = new CommandBase(ExecuteCreateJwkCommand);
            SelectCertificateCommand = new CommandBase(ExecuteSelectCertificateCommand);
            
            _jwkCreator = jwkCreator;
            _pubKeyHandler = pubKeyHandler;
            AlgorithmDropDown = config.rsaKeys;
     
        }


        public string SelectedFilePath
        {
            get { return _selectedFilePath; }
            set { _selectedFilePath = value; OnPropertyChanged("SelectedFilePath"); }
        }

        public string FileLabel
        {
            get { return _fileLabel; }
            set { _fileLabel = value; OnPropertyChanged("FileLabel"); }
        }


        public string Algorithm
        {
            get { return _rsaKey; }
            set { _rsaKey = value; OnPropertyChanged("Algorithm"); }
        }

        public string PublicKey
        {
            get { return _publicKey; }
            set { _publicKey = value; OnPropertyChanged("PublicKey"); }
        }


        public string Kid
        {
            get { return _kid; }
            set { _kid = value; OnPropertyChanged("Kid"); }
        }

        public string Jwk
        {
            get { return _jwk; }
            set { _jwk = value; OnPropertyChanged("Jwk"); }
        }

        public List<string> AlgorithmDropDown { get; }

        public ICommand SelectFileCommand { get; private set; }
        public ICommand SelectCertificateCommand { get; private set; }
        public ICommand CreateJwkFileCommand { get; private set; }
        public ICommand TestCommand { get; private set; }


        private void ExecuteSelectCertificateCommand(object obj)
        {

            if (!PreValidateInput()) return;

            var key = _pubKeyHandler.GetPublicKeyFromSertificate(Kid);

            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Could not find Certificate: " + Kid, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var response = _jwkCreator.CreateJwk(key, Kid, Algorithm);

            if (response.Success == true && response.DigDirJwkString != null)
            {
                Jwk = response.DigDirJwkString;
            }
            else
            {

                MessageBox.Show(response.ErrorCode, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return;
        }

        private void ExecuteCreateJwkCommand(object obj)
        {

            Kid = Kid.ToUpper();
            //var response = _jwkCreator.CreateJwk(_publicKey, Kid, Algorithm);

            if (!string.IsNullOrEmpty(Jwk))
            {
                if (_jwkCreator.SaveToFile(Jwk))
                {
                    MessageBox.Show("Result saved successfully!", "Success");
                }
            }
            else
            {
                MessageBox.Show("Failed to save Jwk to file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return;
        }

        private bool PreValidateInput()
        {
            if (string.IsNullOrEmpty(Kid))
            {

                MessageBox.Show("Enter Certificate Kid!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            if (string.IsNullOrEmpty(Algorithm))
            {
                MessageBox.Show("Select RSA Key!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
            return true;
        }

        private void ExecuteSelectFileCommand(object obj)
        {
            if (!PreValidateInput()) return;

            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedFilePath = openFileDialog.FileName;
                FileLabel = "Selected File: " + openFileDialog.SafeFileName;
                PublicKey = _pubKeyHandler.GetPublicKey(_selectedFilePath);
            }
            else
            {
                return;
            }

            var response = _jwkCreator.CreateJwk(_publicKey, Kid, Algorithm);

            if (response.Success == true && response.DigDirJwkString != null)
            {
                Jwk = response.DigDirJwkString;
            }
            else
            {
                MessageBox.Show(response.ErrorCode, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return;

        }

        //private void ExecuteTestCommand(object obj)
        //{

        //    TestPageView w = new TestPageView();

        //    w.Show();



        //}
    }
}
