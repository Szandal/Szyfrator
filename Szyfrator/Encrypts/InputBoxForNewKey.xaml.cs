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
using System.Windows.Shapes;

namespace Encrypt.Encrypts
{
    /// <summary>
    /// Logika interakcji dla klasy InputBoxForNewKey.xaml
    /// </summary>
    public partial class InputBoxForNewKey : Window
    {
        public InputBoxForNewKey(string question)
        {
            InitializeComponent();
            this.question.Text = question;
        }
        private void OKOn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Answer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsText(((TextBox)sender).Text + e.Text);
        }
        public static bool IsText(string str)
        {
            foreach(char letter in str)
            {
                
                if (!Char.IsLetter(letter) && letter != ' ')
                {
                    
                    return false;
                }
            }
            return true;
        }

    }
}
