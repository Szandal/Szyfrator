using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt.Print
{
    class AddToPrint

    {
        private List<string> textToPrint;

        public AddToPrint()
        {
            textToPrint = new List<string>();
        }

        public void AddToDictionary(string explicitText, string nonpublicText)
        {
            int key;
            if(textToPrint.Count == 0)
            {
                key = 0;
            }
            else
            {
                key = textToPrint.Count;
            }
            textToPrint.Add(item: explicitText);
            textToPrint.Add(item: nonpublicText);
        }

        public List<string> GetToPrint() => textToPrint;

    }
}
