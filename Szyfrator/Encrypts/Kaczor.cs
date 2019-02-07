using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Encrypt.Encrypts
{
    class Kaczor : Encrypt
    {
        private string key = "ACKORZ";  // THIS  IS ANAGRAM "KACZOR"
        private string[] dividedAlphabet;
        public Kaczor()
            : base()
        {
            dividedAlphabet = new string[key.Length];
            for(int i=0; i<key.Length; i++)
            {
                if(i+1!=key.Length)
                {
                    dividedAlphabet[i] = polishAlphabet.Substring(polishAlphabet.IndexOf(key[i]), (polishAlphabet.IndexOf(key[i + 1]) - polishAlphabet.IndexOf(key[i]) ) );
                }
                else
                {
                    dividedAlphabet[i] = polishAlphabet.Substring(polishAlphabet.IndexOf(key[i]), polishAlphabet.Length - polishAlphabet.IndexOf(key[i]));
                }                
            }
        }

        public string Encryption(string explicitText)
        {
            nonpublicText = "";
            char letter;
            for(int i = 0; i<explicitText.Length; i++)
            {
                if(char.IsLetter(explicitText[i]))
                {
                    letter = char.ToUpper(explicitText[i]);
                    nonpublicText += SearchLetterAndNumberForSwitch(letter);
                }
                else
                {
                    nonpublicText += explicitText[i];
                }
            }
            return nonpublicText;
        }

        private string SearchLetterAndNumberForSwitch(char letter)
        {
            for(int i = 0; i<key.Length; i++)
            {
                if(dividedAlphabet[i].Contains(letter))
                {
                    return  dividedAlphabet[i][0].ToString()+(dividedAlphabet[i].IndexOf(letter)+1).ToString();
                }
            }
            return letter + "";
        }

        public string Decryption(string nonpublicText)
        {
            explicitText = "";
            for(int i = 0; i<nonpublicText.Length; i++)
            {
                if(!char.IsLetter(nonpublicText[i]))
                {
                    explicitText += nonpublicText[i];
                }
                else
                {
                    int index;
                    if(nonpublicText[i]=='C'&& nonpublicText[i+1] == '1'&& nonpublicText[i+2] == '0')
                    {
                        index = 10;
                    }
                    else
                    {
                        index = (int)Char.GetNumericValue(nonpublicText[i+1]);   
                    }

                    explicitText += SearchForLetter(nonpublicText[i],index);

                    if (index == 10)
                    {
                        i += 2;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return explicitText;
        }

        private char SearchForLetter(char letter, int index)
        {
            letter = char.ToUpper(letter);
            int keyIndex = key.IndexOf(letter);
            try
            {
                return dividedAlphabet[keyIndex][index-1];
            }
            catch
            {
                MessageBox.Show("nie istnieje " + letter + index);
                return '*';
            }                               
        }
    }
}
