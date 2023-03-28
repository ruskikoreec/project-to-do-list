using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    internal interface ICripter
    {
        string Encryption(string input);
        string Decryption(string input);
    }
    internal class EnDeCryption : ICripter
    {
        public string Encryption(string input)
        {
            char[] Cesar = input.ToCharArray();
            for (int i = 0; i < Cesar.Length; i++)
            {
                Cesar[i] = (char)(Cesar[i] + 10);
            }
            string output = new string(Cesar);
            return output;
        }
        public string Decryption(string input)
        {
            char[] CesarBack = input.ToCharArray();
            for (int i = 0; i < CesarBack.Length; i++)
            {
                CesarBack[i] = (char)(CesarBack[i] - 10);
            }
            string output = new string(CesarBack);
            return output;
        }
    }
}
