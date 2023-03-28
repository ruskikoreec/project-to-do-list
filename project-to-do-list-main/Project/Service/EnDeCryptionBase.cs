namespace Project.Service
{
    internal class EnDeCryptionBase
    {
        internal static string Decryption(string input)
        {
            char[] CesarBack = input.ToCharArray();
            for (int i = 0; i < CesarBack.Length; i++)
            {
                CesarBack[i] = (char)(CesarBack[i] - 10);
            }
            string output = new string(CesarBack);
            return output;
        }
        internal static string Encryption(string input)
        {
            char[] Cesar = input.ToCharArray();
            for (int i = 0; i < Cesar.Length; i++)
            {
                Cesar[i] = (char)(Cesar[i] + 10);
            }
            string output = new string(Cesar);
            return output;
        }
    }
}