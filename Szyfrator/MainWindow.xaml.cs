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
using Encrypt.Print;
using System.Diagnostics;
using Encrypt.ToFile;
using System.IO;

namespace Encryption
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddToPrint atp;
        string fileContent = string.Empty;
        //create every optional controls 
        ComboBox optionalComboBox = new ComboBox();
        private List<string> encryptList;
        public MainWindow()
        {
            InitializeComponent();
            LoadEncryptToComboBox();
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
        private int GetShiftValue(string errorMessage = "Wybierz przesunięcie drogi użytkowniku")
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
            string explicitText;
            if(inputField.IsEnabled == false)
            {
                explicitText = fileContent;
            }
            else
            {
                explicitText = inputField.Text.ToString();
            }
            string outText = string.Empty;
            
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
                    outText = cesar.Encryption(explicitText,shift);
                    break;
                case "Kaczor":
                    Kaczor duck = new Kaczor();
                    outText = duck.Encryption(explicitText);
                    break;
                case "Morse'a":
                    Morse morse = new Morse();
                    outText = morse.Encryption(explicitText);
                    break;
                case "Liczbowy":
                    Numeric numeric = new Numeric();
                    outText = numeric.Encryption(explicitText);
                    break;
                case "Cyfrowy":
                    Digital digital = new Digital();
                    int shiftDigit = GetShiftValue();
                    if(shiftDigit == -1)
                    {
                        break;
                    }
                    string password;
                    var dialog = new InputBoxForNumericPass("Podaj hasło numeryczne");
                    if (dialog.ShowDialog() == true)
                    {
                        password = dialog.answer.ToString();
                    }
                    else
                    {
                        break;
                    }
                    outText = digital.Encryption(explicitText,shiftDigit,password);
                    break;
                case "Zamiennikowy":
                    Zamiennikowy zamiennikowy = new Zamiennikowy();
                    string key = GetEncryptKeyFromComboBox();
                    if(key.Equals("Wybierz Klucz..."))
                    {
                        break;
                    }
                    outText = zamiennikowy.Encryption(explicitText,key);
                    break;
            }
            if(inputField.IsEnabled != false)
            {
                outputField.Text = outText;                  
            }
            else
            {
                EncryptFile encryptFile = new EncryptFile();
                encryptFile.SaveNewFile(outText);
                SetEnableTextBox();
            }
        }        
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText;
            if (inputField.IsEnabled == false)
            {
                inputText = fileContent;
            }
            else
            {
                inputText = inputField.Text.ToString();
            }
            string outText = string.Empty;
            switch (chooseType.Text)
            {
                case "Cezar":
                    Cesar cesar = new Cesar();
                    int shift = GetShiftValue();
                    if (shift == -1)
                    {
                        break;
                    }
                    outText = cesar.Encryption(inputText, -shift);
                    break;
                case "Kaczor":
                    Kaczor duck = new Kaczor();
                    outText = duck.Decryption(inputText);
                    break;
                case "Morse'a":
                    Morse morse = new Morse();
                    outText = morse.Decrytption(inputText);
                    break;
                case "Liczbowy":
                    Numeric numeric = new Numeric();
                    outText = numeric.Decrytption(inputText);
                    break;
                case "Cyfrowy":
                    Digital digital = new Digital();
                    int shiftDigit = GetShiftValue();
                    if (shiftDigit == -1)
                    {
                        break;
                    }
                    string password;
                    var dialog = new InputBoxForNumericPass("Podaj hasło numeryczne");
                    if (dialog.ShowDialog() == true)
                    {
                        password = dialog.answer.Text.ToString();
                    }
                    else
                    {
                        break;
                    }
                    outText = digital.Decryption(inputText, GetShiftValue(),password);
                    break;
                case "Zamiennikowy":
                    Zamiennikowy zamiennikowy = new Zamiennikowy();
                    string key = GetEncryptKeyFromComboBox();
                    if (key.Equals("Wybierz Klucz..."))
                    {
                        break;
                    }
                    outText = zamiennikowy.Encryption(inputText,key);
                    break;
            }
            if (inputField.IsEnabled != false)
            {
                outputField.Text = outText;
            }
            else
            {
                EncryptFile encryptFile = new EncryptFile();
                encryptFile.SaveNewFile(outText);
                SetEnableTextBox();

            }
        }
        private void SetEnableTextBox()
        {
            inputField.IsEnabled = true;
            outputField.IsEnabled = true;
            fromFile.Content = "Szyfruj z pliku";
            inputField.Text = "";
        }
        private void CheckTime(Stopwatch stopwatch, string encryptType, string type)
        {
            MessageBox.Show(encryptType + " " + type +'\t' + stopwatch.Elapsed.TotalMilliseconds.ToString("000000.00000 ns") + '\t');   
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
        private void FromFileText(object sender, RoutedEventArgs e)
        { 
            if(fromFile.Content.ToString() == "Anuluj")
            {
                SetEnableTextBox();
                return;
            }
            else
            {
                EncryptFile encryptFile = new EncryptFile();
                if (encryptFile.OpenFile())
                {
                    fromFile.Content = "Anuluj";
                    inputField.Text = "Szyfrowanie z pliku, wybierz sposób szyfrowania";
                    inputField.IsEnabled = false;
                    outputField.Text = "";
                    outputField.IsEnabled = false;
                    fileContent = encryptFile.GetFileContent();
                }
            }

            
        }
    }
}
