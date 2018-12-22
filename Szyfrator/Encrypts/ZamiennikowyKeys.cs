using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt.Encrypts
{
    class ZamiennikowyKeys
    {
        public string keyName { get; private set; }
        public string key { get; private set; }
        
        public ZamiennikowyKeys(string[] key)
        {
            this.keyName = key[0];
            this.key = key[1];
        }
    }
}
