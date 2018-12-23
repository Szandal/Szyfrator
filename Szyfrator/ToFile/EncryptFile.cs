using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Encrypt.ToFile
{
    class EncryptFile
    {
        private string importFileName, importFileContent;
        public EncryptFile()
        {
            importFileName = string.Empty;
            importFileContent = string.Empty;
        }
        public string GetFileContent() => importFileContent;
        public bool OpenFile()
        {
            OpenDialog();
            if (importFileName == string.Empty)
            {
                MessageBox.Show("Nie wybrano poprawnego pliku");
                return false;
            }
            importFileContent = System.IO.File.ReadAllText(importFileName);
            return true;
        }
        private void OpenDialog()
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "txt files (*.txt)|*.txt",
                Title = "Otwórz plik",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (OFD.ShowDialog() == true)
            {
                importFileName = OFD.FileName;
            }
        }

        public void SaveNewFile(string content)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt",
                Title = "Zapisz Plik"
            };
            saveDialog.ShowDialog();
            if(saveDialog.FileName != "")
            {
                FileStream fs = (FileStream)saveDialog.OpenFile();
                try
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(content);
                    sw.Close();

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }


    }
}
