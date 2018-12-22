using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Encrypt.Encrypts
{
    class Morse : Encrypt
    {
        private string numbers = "0123456789";
        private string symbols = " .,:?!";
        private string[] morsePolishAlphabet = { ".-", ".-.-", "-...", "-.-.", "-.-..", "-..", ".", "..-..", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", ".-..-", "--", "-.", "--.--", "---", "---.", ".--.", ".-.", "...", "...-...", "-", "..-", ".--", "-.--", "--..", "--..-", "--..-." };
        private string[] morseNumbers = { "-----", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----." };
        private string[] morseSymbols = { "", ".-.-.-", "--..--", "---...", "..--..", "-.-.--" };


        public Morse()
           : base()
        {

        }
        public string Encryption(string explicitText)
        {
            char letter;
            nonpublicText = "";
            for(int i = 0; i<explicitText.Length; i++)
            {

                letter = Char.ToUpper(explicitText[i]);
                if (polishAlphabet.Contains(letter))
                {
                    nonpublicText += morsePolishAlphabet[polishAlphabet.IndexOf(letter)];
                }
                else
                if(numbers.Contains(letter))
                {
                    nonpublicText += morseNumbers[numbers.IndexOf(letter)];
                }
                else
                if(symbols.Contains(letter))
                {
                    nonpublicText += morseSymbols[symbols.IndexOf(letter)];
                }
                else
                {
                    nonpublicText += letter;
                }
                nonpublicText += "/";
            }
            return nonpublicText;
        }

        public string Decrytption(string nonpublicText)
        {
            explicitText = "";
            int index;
            string[] letters = nonpublicText.Split('/');
            foreach (string letter in letters)
            {
                if(morsePolishAlphabet.Contains(letter))
                {
                    index = Array.IndexOf(morsePolishAlphabet,letter);
                    explicitText += polishAlphabet[index];
                }else
                if(morseNumbers.Contains(letter))
                {
                    index = Array.IndexOf(morseNumbers, letter);
                    explicitText += numbers[index];
                }else
                if(morseSymbols.Contains(letter))
                {
                    index = Array.IndexOf(morseSymbols, letter);
                    explicitText += symbols[index];
                }else
                {
                    explicitText += letter;
                }                
            }
            return explicitText;
        }
    }
}
