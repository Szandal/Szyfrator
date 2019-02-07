using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt.Encrypts
{
    abstract class Encrypt
    {
        protected string latinAlphabet, latinLowerAlphabet, polishAlphabet, polishLowerAlphabet = "", explicitText, nonpublicText;

        public Encrypt()
        {
            latinAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            polishAlphabet = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPRSŚTUWXYZŹŻ";
            polishLowerAlphabet = polishAlphabet.ToLower();
            latinLowerAlphabet = latinAlphabet.ToLower();            
        }
        public void GetExplicitText(string explicitText)
        {
            this.explicitText = explicitText;
        }

        protected char PolishToLatinLetter(char letter)
        {
            string polishLetters = "ĄĆĘŁŃÓŚŹŻ";
            string latinLetters = "ACELNOSZZ";
            string polishLowerLetters = polishLetters.ToLower();
            string latinLowerLetters = latinLetters.ToLower();
            if(polishLetters.Contains(letter))
            {
                letter = latinLetters[polishLetters.IndexOf(letter)];
            }
            if(polishLowerLetters.Contains(letter))
            {
                letter = latinLowerLetters[polishLowerLetters.IndexOf(letter)];
            }
            return letter;
        }

        protected string PolishToLatinLetter(string str)
        {
            string result = "";
            foreach(char letter in str)
            {
                result += PolishToLatinLetter(letter);
            }
            return result;            
        }
    }
}
