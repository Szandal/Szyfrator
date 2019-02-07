using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;

namespace Encrypt.Print
{
    /// <summary>
    /// Logika interakcji dla klasy PreparePrint.xaml
    /// </summary>
    

    public partial class PreparePrint : Window
    {
        private  Grid grid;
        private List<string> list;
        List<CheckBox> checkBoxList;
        public PreparePrint(List<string> list)
        {
            InitializeComponent();
            this.list = new List<string>();
            this.list = list;
            grid = new Grid();
            placeForCB.Children.Add(grid);
            checkBoxList = new List<CheckBox>();
            LoadCheckBox();

        }
       
        private string ListToString(List<string> list)
        {
            string result = string.Empty;
            foreach(string s in list)
            {
                result += s;
                result += "\n\n\n";
            }
            return result;
        }

        private void LoadCheckBox()
        {            
            for (int i = 0; i < list.Count(); i++)
            {
                CheckBox cb = new CheckBox();
                TextBlock tb = new TextBlock
                {
                    Text = list[i],
                    TextWrapping = TextWrapping.Wrap,
                    Padding = new Thickness(0, 0, 10, 0)
                };
                cb.Content = tb;
                checkBoxList.Add(cb);
                grid.Children.Add(checkBoxList.ElementAt(i));
                grid.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(checkBoxList.ElementAt(i),i);                
            }
        }
      
        private List<string> GetListToPrint()
        {
            List<string> listToPrint = new List<string>();
            foreach(CheckBox cb in checkBoxList)
            {
                if(cb.IsChecked == true)
                {
                    TextBlock tb = (TextBlock)cb.Content;                   
                    listToPrint.Add(tb.Text);
                }
            }
            return listToPrint;
        }
        private bool IsListEmpty(List<string> list)
        {
            if (list.Count==0)
            {
                MessageBox.Show("Nie zaznaczono nic");
                return true;
            }
            return false;
        }
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            List<string> listToPrint = new List<string>();
            listToPrint = GetListToPrint();
            //zabezpieczenie przed pustą listą
            if(IsListEmpty(listToPrint))
            {
                return;
            }
            Printing(listToPrint);
        }

        private void Printing(List<string> list)
        {
            
            string text = ListToString(list);
            PrintDialog printDialog = new PrintDialog
            {
                PageRangeSelection = PageRangeSelection.AllPages,
                UserPageRangeEnabled = true
            };
            FlowDocument doc = new FlowDocument(new Paragraph(new Run(text)))
            {
                Name = "PrintEncrypt"
            };
            Nullable<Boolean> print = printDialog.ShowDialog();
            if(print == true)
            {
                IDocumentPaginatorSource idps = doc;
                printDialog.PrintDocument(idps.DocumentPaginator, "PrintEncrypt");
            }           
            
        }
       
    
    }
}
