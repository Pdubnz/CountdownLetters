using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountdownLettersWPF.Helpers
{
    public interface IDictionary
    {
        List<string> Anagrams { get; set; }
        List<string> Words { get; set; }

        bool CheckSpelling(string word);
        Task<List<string>> GetAnagrams(string inputText);

        IEnumerable<string> GetLocalAnagrams(string inputText);
    }
}