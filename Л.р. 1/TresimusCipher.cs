using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Л.р._1
{
    public class TresimusCipher
    {
        private static string alphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private static char[,] table = new char[4, 8];
        public static char[,] GenerateTable (string key)
        {
            string uniqueChars = new string((key.ToUpperInvariant() + alphabet).ToCharArray().Distinct().ToArray());
            int index = 0;
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    table[i, j] = uniqueChars[index++];
                }
            }
            return table;
        }
        private static (int, int) FindPosition(char[,] table, char c)
        {
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (table[i, j] == c)
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }
        public static string Encrypt(string inputString, string key)
        {
            table = GenerateTable(key);
            StringBuilder result = new StringBuilder();

            foreach (char c in inputString)
            {
                if (c == ' ')
                {
                    result.Append(' ');
                    continue;
                }
                var (row, col) = FindPosition(table, c);
                if (row != -1 && col != -1)
                {
                    result.Append(table[(row + 1) % 4, col]);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }
}
