using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Encrypt.Encrypts
{
    class Digital : Encrypt

    {
        public Digital()
            : base()
        { 
        }
        public string FindNumber(string text)
        {
            string result = "";
            foreach (char letter in text)
            {
                if (Char.IsDigit(letter))
                {
                    result += letter;
                }
            }
            if (result == "")
            {
                return "0";
            }
            return result;
        }
        public string Encryption(string explicitText, int shift, string password = "0")
        {
            password = FindNumber(password);
            nonpublicText = "";
            int j = 0;
            for(int i=0; i<explicitText.Length; i++)
            {
                char latinLetter = PolishToLatinLetter(char.ToUpper(explicitText[i]));
                if (Char.IsLetter(latinLetter))
                {
                    int withoutPass = (latinAlphabet.IndexOf(Char.ToUpper(latinLetter)) + 1 - shift);
                    if (withoutPass <= 0)
                    {
                        withoutPass += latinAlphabet.Length;
                    }
                    char p = password[j % password.Length];
                    Debug.WriteLine(p);
                    nonpublicText += (withoutPass + int.Parse(p.ToString()));
                        j++;
                }
                else
                {
                    nonpublicText += latinLetter;
                }
                nonpublicText += "/";
            }
            return nonpublicText;
        }
        public string Decrytption(string nonpublicText,int shift , string password = "0" )
        {
            password = FindNumber(password);
            explicitText = "";
            int j = 0;
            string[] letters = nonpublicText.Split('/');
            for (int i = 0; i<letters.Length; i++)
            {
                int number;
                if (Int32.TryParse(letters[i], out number))
                {
                    number -= (int)Char.GetNumericValue(password[j % password.Length]);
                    j++;
                    if (number>latinAlphabet.Length)
                    {
                        number -= latinAlphabet.Length;
                    }
                    if (number - 1 <= latinAlphabet.Length && number > 0)
                    {
                        explicitText += latinAlphabet[(number - 1 + shift)%latinAlphabet.Length ];
                        continue;
                    }
                }
                explicitText += letters[i];
            }
            return explicitText;
        }
    }

}
