using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Encrypt.Encrypts
{
    class Cesar : Encrypt
    {
        public Cesar() 
            : base()
        {

        }

         public string Encryption(string explicitText, int shift)
        {
            nonpublicText = "";
            for(int i=0; i<explicitText.Length; i++)
            {
                if(char.IsLetter(explicitText[i]) && char.IsLower(explicitText[i]))
                {
                    nonpublicText += SwitchLetter(explicitText[i], shift, polishLowerAlphabet);
                }
                else
                if(char.IsLetter(explicitText[i]) && char.IsUpper(explicitText[i]))
                {
                    nonpublicText += SwitchLetter(explicitText[i], shift, polishAlphabet);
                }
                else
                {
                    nonpublicText += explicitText[i];
                }
            }
            return nonpublicText;
        }

        private char SwitchLetter(char letter, int shift, string alphabet)
        {
            while(shift < 0)
            {
                shift = alphabet.Length + shift;
            }
            int location = alphabet.IndexOf(letter);
            if (location<0)
            {
                return letter;
            }
            return alphabet[(location + shift) % alphabet.Length];
        }
    }
}
