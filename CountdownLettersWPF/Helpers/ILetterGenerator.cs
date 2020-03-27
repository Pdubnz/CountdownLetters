using System.Collections.Generic;

namespace CountdownLettersWPF.Helpers
{
    public interface ILetterGenerator
    {
        List<char> Consonants { get; set; }
        List<char> Vowels { get; set; }

        char GetConsonant();
        char GetVowel();
    }
}