using System.Collections.Generic;
using System.Threading.Tasks;

namespace CountdownLettersWPF.API
{
    public interface IAnagramsEndpoint
    {
        Task<List<string>> GetAnagrams(string text);
    }
}