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
        private string GetDigitalFromText(string text)
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
            password = GetDigitalFromText(password);
            nonpublicText = "";
            
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
                    char passwordSign = password[i % password.Length];
                    nonpublicText += (withoutPass + int.Parse(passwordSign.ToString()));
                    
                }
                else
                {
                    nonpublicText += latinLetter;
                }
                nonpublicText += "/";
            }
            return nonpublicText;
        }
        public string Decryption(string nonpublicText,int shift , string password = "0" )
        {
            password = GetDigitalFromText(password);
            explicitText = "";
            string[] letters = nonpublicText.Split('/');
            for (int i = 0; i<letters.Length; i++)
            {
                int numberOfNonpublicLetter;
                if (Int32.TryParse(letters[i], out numberOfNonpublicLetter))
                {
                    numberOfNonpublicLetter -= (int)Char.GetNumericValue(password[i % password.Length]);
                    while (numberOfNonpublicLetter>latinAlphabet.Length)
                    {
                        numberOfNonpublicLetter -= latinAlphabet.Length;
                    }
                    if (numberOfNonpublicLetter - 1 <= latinAlphabet.Length && numberOfNonpublicLetter > 0)
                    {
                        explicitText += latinAlphabet[(numberOfNonpublicLetter - 1 + shift)%latinAlphabet.Length ];
                        continue;
                    }
                }
                explicitText += letters[i];
            }
            return explicitText;
        }
    }

}
