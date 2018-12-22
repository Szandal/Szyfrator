using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Encrypt
{
    /// <summary>
    /// Logika interakcji dla klasy DigitPassword.xaml
    /// </summary>
    public partial class InputBoxForNumericPass : Window
    {
        public InputBoxForNumericPass(string question)
        {
            InitializeComponent();
            this.question.Text = question;
        }

        public string AnwswerText
        {
            get { return answer.Text; }
            set { answer.Text = value; }
        }

        private void OKOn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void answer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(((TextBox)sender).Text + e.Text);
        }
        public static bool IsTextNumeric(string str)
        {
            int i;
            return int.TryParse(str, out i);
        }


    }
}
