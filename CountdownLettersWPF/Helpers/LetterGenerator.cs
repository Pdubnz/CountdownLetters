using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountdownLettersWPF.Helpers
{
    public class LetterGenerator : ILetterGenerator
    {
        public List<char> Vowels { get; set; } = new List<char>();
        public List<char> Consonants { get; set; } = new List<char>();

        public Random random = new Random();
        public char GetVowel()
        {
            char[] vowels = new char[]{ 'A', 'E', 'I', 'O', 'U', 'E',
                                        'E', 'I', 'O', 'U', 'E', 'A',
                                        'I', 'O', 'U', 'E', 'A', 'E',
                                        'O', 'U', 'E', 'A', 'E', 'I',
                                        'U', 'E', 'A', 'E', 'I', 'O'};
            int loc = random.Next(0, vowels.Length);
            return vowels[loc];
        }
        public char GetConsonant()
        {
            char[] consonants = new char[] { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P',
                                             'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z', 'C', 'D', 'F',
                                             'G', 'H', 'L', 'M', 'N', 'P', 'S', 'S', 'R', 'R', 'T', 'T',
                                             'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W',
                                             'X', 'Y', 'Z', 'C', 'D', 'F', 'G', 'H', 'L', 'M', 'N', 'P',
                                             'S', 'S', 'R', 'R', 'T', 'T', 'B', 'C', 'D', 'F', 'G', 'H',
                                             'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z', 'C', 'D', 'F',
                                             'G', 'H', 'L', 'M', 'N', 'P', 'S', 'S', 'R', 'R', 'T', 'T',
                                             'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P',
                                             'X', 'Y', 'Z', 'C', 'D', 'F', 'G', 'H', 'L', 'M', 'N', 'P',
                                             'S', 'S', 'R', 'R', 'T', 'T', 'B', 'C', 'D', 'F', 'G', 'H',
                                             'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W',
                                             'G', 'H', 'L', 'M', 'N', 'P', 'S', 'S', 'R', 'R', 'T', 'T',
                                             'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P',
                                             'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z', 'C', 'D', 'F',
                                             'S', 'S', 'R', 'R', 'T', 'T', 'B', 'C', 'D', 'F', 'G', 'H',
                                             'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W',
                                             'X', 'Y', 'Z', 'C', 'D', 'F', 'G', 'H', 'L', 'M', 'N', 'P' };
            int loc = random.Next(0, consonants.Length);
            return consonants[loc];
        }

    }

}
