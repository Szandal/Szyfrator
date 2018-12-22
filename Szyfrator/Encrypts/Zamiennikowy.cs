using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Encrypt;
namespace Encrypt.Encrypts
{
    class Zamiennikowy : Encrypt
    {
        const char Separator = '|';
        const String Location = "data\\zamiennik.bin";
        public Zamiennikowy()
            :base()
        {
            if(!File.Exists(Location))
            {
                CreateNewFile();
            }
        }

        public string Encryption(string explicitText, string keyName)
        {
            List<ZamiennikowyKeys> list = GetListOfKeys();
            ZamiennikowyKeys key = list.Find(fkey => fkey.keyName == keyName);
            nonpublicText = "";
            foreach (char letter in explicitText)
            {
                if (Char.IsLetter(letter))
                {
                    if(Char.IsLower(letter))
                    {
                        nonpublicText += Char.ToLower(key.key[latinLowerAlphabet.IndexOf(PolishToLatinLetter(letter))]);
                       
                    }
                    else
                    {
                        nonpublicText += key.key[latinAlphabet.IndexOf(PolishToLatinLetter(letter))];
                    }
                }
                else
                {
                    nonpublicText += letter;
                }        
            }
            return nonpublicText;
        }

        

        public List<ZamiennikowyKeys> GetListOfKeys()
        {
            List<ZamiennikowyKeys> list = new List<ZamiennikowyKeys>();
            FileStream fs = new FileStream(Location, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line;
            
            while((line = sr.ReadLine()) != null)
            {
                ZamiennikowyKeys zamiennikowyKey = new ZamiennikowyKeys(line.Split(Separator));
                list.Add(zamiennikowyKey);
            }
            
            return list;
        }

        protected void CreateNewFile()
        {
            Directory.CreateDirectory("data");
            using (StreamWriter sw = new StreamWriter(Location))
            {
                sw.WriteLine("Gadery Poluki"+Separator+"GBCEDFAHKJIUMNPOYSTLWXRZ");
                sw.WriteLine("Koniec Matury" + Separator + "MBEDCFGHNJOLAIKPYSUTWXRZ");
                sw.WriteLine("KaCe Minutowy" + Separator + "KBEDCFGHMJALIUTPRSONYXWZ");
                sw.WriteLine("Malinowe Buty" + Separator + "MUCDWFGHLJKIAONPRSYBEXTZ");
                sw.WriteLine("Nowe Buty Lisa" + Separator + "SUCDWFGHLJKIMONPRAYBEXTZ");
                sw.WriteLine("Nasz Hufiec" + Separator + "NBEDCIGUFJKLMAOPRZTHWXYS");
            }
        }

        public void AddNewKeyToFile(string question = "podaj nowy klucz:")
        {
            string newKeyName = "", newKey = "";
            var input = new InputBoxForNewKey(question);
            if (input.ShowDialog() == true)
            {
                newKeyName = input.answer.Text.ToString();
                newKey = RemoveSpace(newKeyName);
                newKey = newKey.ToUpper();
                newKey = PolishToLatinLetter(newKey);
                ValidateKey(newKey);
                newKey = GetNewAlphabet(newKey);
                File.AppendAllText(Location, newKeyName + Separator + newKey + '\n');
            }


        }

        private string GetNewAlphabet(string newKey)
        {
            string result = "";
            for(int i = 0; i < latinAlphabet.Length; i++)
            {
                if (newKey.Contains(latinAlphabet[i]))
                {
                    int index = newKey.IndexOf(latinAlphabet[i]);
                    int shift = index + (index % 2 == 0?1:-1);
                    result += newKey[shift];

                }
                else
                {
                    result += latinAlphabet[i];
                }
            }
            return result;
        }

        private string RemoveSpace(string key)
        {
            int placeOfSpace = key.IndexOf(' ');
            if (placeOfSpace != -1)
            {
                key = key.Remove(placeOfSpace, 1);
                
                key = RemoveSpace(key);
            }
            return key;
        }

        private void ValidateKey(string key)
        {
            if (key.Equals(""))
            {
                AddNewKeyToFile("nie podałeś klucza,\nspróbuj jeszcze raz:");
                return;
            }
            if ((key.Length % 2) == 1)
            {
                AddNewKeyToFile("podałeś błędny klucz,\nspróbuj jeszcze raz:");
                return;
            }
            if (DubbleLetter(key))
            {
                AddNewKeyToFile("podałeś błędny klucz,\npodwójne wystąpienie litery,\nspróbuj jeszcze raz:");
                return;
            }
        }

        private bool DubbleLetter(string key)
        {
            foreach(char letter in key)
            {
                if (key.IndexOf(letter) != key.LastIndexOf(letter))
                {
                    Debug.WriteLine(letter);
                    return true;
                }
            }
            return false;
        }
    }


}
