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
using Encrypt;
using Encrypt.Encrypts;
using System.Diagnostics;


namespace Encryption
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddToPrint atp;
        //create every optional controls 
        ComboBox optionalComboBox = new ComboBox();
        private List<string> encryptList;
        public MainWindow()
        {
            InitializeComponent();
            LoadEncryptToComboBox();
            //set every optional controls
            optionalControlPlace.Children.Add(optionalComboBox);
            Zamiennikowy zam = new Zamiennikowy();
            atp = new AddToPrint();
        }
        

        private void LoadEncryptToComboBox()
        {
            LoadEncryptList();
            foreach(string encryptType in encryptList)
            {
                chooseType.Items.Add(encryptType);
            }
        }
        private void LoadEncryptList()
        {
            encryptList = new List<string>
            {
                "Wybierz szyfr",
                "Cezar",
                "Cyfrowy",
                "Kaczor",
                "Liczbowy",
                "Morse'a",
                "Zamiennikowy"
            };
        }
        private void ShowOptionalControl(object sender, SelectionChangedEventArgs e)
        {
            switch(chooseType.SelectedValue.ToString())
            {
                case "Cezar":
                    AddShiftValuesToComboBox(32);
                    optionalComboBox.Visibility = Visibility.Visible;
                    addKey.Visibility = Visibility.Hidden;
                    break;
                case "Zamiennikowy":
                    AddEncryptsKeysToComboBox();
                    optionalComboBox.Visibility = Visibility.Visible;
                    addKey.Visibility = Visibility.Visible;
                    break;
                case "Cyfrowy":
                    AddShiftValuesToComboBox(24);
                    optionalComboBox.Visibility = Visibility.Visible;
                    addKey.Visibility = Visibility.Hidden;
                    break;
                default:
                    optionalComboBox.Visibility = Visibility.Hidden;
                    addKey.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void AddEncryptsKeysToComboBox()
        {
            optionalComboBox.Items.Clear();
            optionalComboBox.Items.Add("Wybierz Klucz...");
            Zamiennikowy zamiennikowy = new Zamiennikowy();
            List<ZamiennikowyKeys> list = zamiennikowy.GetListOfKeys();
            foreach(ZamiennikowyKeys keyName in list)
            {
                optionalComboBox.Items.Add(keyName.keyName);
            }

            optionalComboBox.SelectedIndex = 0;
        }

        private string GetEncryptKeyFromComboBox()
        {
            string keyName = "";
            if(optionalComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Wybierz klucz lub dodaj własny");
            }
            keyName = optionalComboBox.SelectedValue.ToString();
            return keyName;
        }

        private void AddShiftValuesToComboBox(int numberOfShifts)
        {
            optionalComboBox.Items.Clear();
            optionalComboBox.Items.Add("Wybierz przesunięcie..");
            for (int i=0; i<=numberOfShifts; i++) 
            {
                optionalComboBox.Items.Add(i);
            }
            optionalComboBox.SelectedIndex = 0;
        }

        public int GetShiftValue(string errorMessage = "Wybierz przesunięcie drogi użytkowniku")
        {
            int shift = 0;
            try
            {
                shift = (int)optionalComboBox.SelectedValue;
            }
            catch
            {
                MessageBox.Show(errorMessage);
                shift = -1;
            }
            return shift;
        }
        

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            string explicitText = inputField.Text.ToString();
            
            switch (chooseType.Text)
            {               
                case "Cezar":
                    int shift;
                    Cesar cesar = new Cesar();
                    shift = GetShiftValue();
                    if(shift==-1)
                    {
                        break;
                    }
                    outputField.Text = cesar.Encryption(explicitText,shift);
                    break;
                case "Kaczor":
                    Kaczor duck = new Kaczor();
                    outputField.Text = duck.Encryption(explicitText);
                    break;
                case "Morse'a":
                    Morse morse = new Morse();
                    outputField.Text = morse.Encryption(explicitText);
                    break;
                case "Liczbowy":
                    Numeric numeric = new Numeric();
                    outputField.Text = numeric.Encryption(explicitText);
                    break;
                case "Cyfrowy":
                    Digital digital = new Digital();
                    int shiftDigit = GetShiftValue();
                    if(shiftDigit == -1)
                    {
                        break;
                    }
                    string password = "0";
                    var dialog = new InputBoxForNumericPass("Podaj hasło numeryczne");
                    if (dialog.ShowDialog() == true)
                    {
                         password = dialog.answer.ToString();
                    }
                    else
                    {
                        break;
                    }
                    outputField.Text = digital.Encryption(explicitText,shiftDigit,password);
                    break;
                case "Zamiennikowy":
                    Zamiennikowy zamiennikowy = new Zamiennikowy();
                    string key = GetEncryptKeyFromComboBox();
                    if(key.Equals("Wybierz Klucz..."))
                    {
                        break;
                    }
                    outputField.Text = zamiennikowy.Encryption(explicitText,key);
                    break;
            }
        }
        
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = inputField.Text.ToString();

            switch (chooseType.Text)
            {
                case "Cezar":
                    Cesar cesar = new Cesar();
                    int shift = GetShiftValue();
                    if (shift == -1)
                    {
                        break;
                    }
                    outputField.Text = cesar.Encryption(inputText, -shift);
                    break;
                case "Kaczor":
                    Kaczor duck = new Kaczor();
                    outputField.Text = duck.Decryption(inputText);
                    break;
                case "Morse'a":
                    Morse morse = new Morse();
                    outputField.Text = morse.Decrytption(inputText);
                    break;
                case "Liczbowy":
                    Numeric numeric = new Numeric();
                    outputField.Text = numeric.Decrytption(inputText);
                    break;
                case "Cyfrowy":
                    Digital digital = new Digital();
                    int shiftDigit = GetShiftValue();
                    if (shiftDigit == -1)
                    {
                        break;
                    }
                    string password = "0";
                    var dialog = new InputBoxForNumericPass("Podaj hasło numeryczne");
                    if (dialog.ShowDialog() == true)
                    {
                        password = dialog.answer.Text.ToString();
                    }
                    else
                    {
                        break;
                    }                    
                    outputField.Text = digital.Decrytption(inputText, GetShiftValue(),password);
                    break;
                case "Zamiennikowy":
                    Zamiennikowy zamiennikowy = new Zamiennikowy();
                    string key = GetEncryptKeyFromComboBox();
                    if (key.Equals("Wybierz Klucz..."))
                    {
                        break;
                    }
                    outputField.Text = zamiennikowy.Encryption(inputText,key);
                    break;
            }
        }

        private void AddNewKey(object sender, RoutedEventArgs e)
        {
            Zamiennikowy zamiennikowy = new Zamiennikowy();
            zamiennikowy.AddNewKeyToFile();
            AddEncryptsKeysToComboBox();
        }

        

        private void AddToPrint(object sender, RoutedEventArgs e)
        {
            atp.AddToDictionary(inputField.Text.ToString(), outputField.Text.ToString());
            MessageBox.Show("Dodano do wydruku");
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Czyta kliknięcie");
            PreparePrint PP = new PreparePrint(atp.GetToPrint());
            PP.Show();
        }

        
    }
}
