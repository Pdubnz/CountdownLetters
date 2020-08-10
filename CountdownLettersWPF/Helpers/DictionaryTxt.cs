using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ZeroFormatter;

namespace CountdownLettersWPF.Helpers
{
    public class DictionaryTxt : IDictionary
    {
        private List<string> _anagrams = new List<string>();
        private List<string> _words = new List<string>();


        static Dictionary<string, List<string>> _dictionary = new Dictionary<string, List<string>>();


        public List<string> Anagrams
        {
            get { return _anagrams; ; }
            set { _anagrams = value; }
        }
        public List<string> Words
        {
            get { return _words; ; }
            set { _words = value; }
        }

        public DictionaryTxt()
        {
            
            
        }
        public void LoadWords()
        {
            try
            {
                //Still to implement
                using (var fs = File.OpenRead("words.txt"))
                {
                    _dictionary = ZeroFormatterSerializer
                        .Deserialize<Dictionary<string, List<string>>>
                        ((new BinaryReader(fs)).ReadBytes((int)fs.Length));
                }
                var prime = _dictionary.ContainsKey(GetKey("test"));
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
        static string GetKey(string w)
        {
            var chars = w.ToCharArray();
            Array.Sort(chars);
            return new string(chars);//.GetHashCode();
        }

        public bool CheckSpelling(string word)
        {
            return _dictionary.ContainsKey(GetKey(word));
        }

        public Task<List<string>> GetAnagrams(string inputText)
        {
            LoadWords();
            return new Task<List<string>>(() => Anagrams = GetLocalAnagrams(inputText).ToList()); 
        }
        public IEnumerable<string> GetLocalAnagrams(string inputText)
        {
            var chars = inputText.ToCharArray();
            Array.Sort(chars);
            var key = new string(chars);
            if (_dictionary.ContainsKey(key))
                foreach (var item in _dictionary[key])
                    yield return item;
            else
                yield break;
        }
    }
}
