using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt.Encrypts
{
    class Numeric : Encrypt
    {
        public Numeric()
          : base()
        {
        }
        public string Encryption(string explicitText, int shift = 0)
        {
            nonpublicText = "";
            foreach(char letter in explicitText)
            {
                char latinLetter = PolishToLatinLetter(char.ToUpper(letter));
                if(Char.IsLetter(latinLetter))
                {
                    nonpublicText += (latinAlphabet.IndexOf(Char.ToUpper(latinLetter))+1+shift);
                }
                else
                {
                    nonpublicText += latinLetter;
                }
                nonpublicText += "/";
            }
            return nonpublicText;
        }

        public string Decrytption(string nonpublicText, int shift = 0)
        {
            explicitText = "";
            string[] letters = nonpublicText.Split('/');
            foreach (string letter in letters)
            {
                int number;
                if(Int32.TryParse(letter,out number))
                {
                    if (number-1<=latinAlphabet.Length && number>0)
                    {
                        explicitText += latinAlphabet[number - 1 - shift];
                        continue;
                    }                         
                }
                explicitText += letter;
                
            }
            return explicitText;
        }
    }
}
