using CountdownLettersWPF.API;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CountdownLettersWPF.Helpers
{
    public class Dictionary : IDictionary
    {

        private List<string> _anagrams = new List<string>();
        private List<string> _words = new List<string>();
        private readonly WordDictionary _dictionary;
        private readonly Spelling _spellChecker;
        private readonly IAnagramsEndpoint _anagramsEndpoint;

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
        /// <summary>
        /// Constructor when not using DI
        /// </summary>
        public Dictionary()
        {
            // Choose what dictionary to use.
            _dictionary = new WordDictionary
            {
                //_dictionary.DictionaryFile = "en-US.dic";
                DictionaryFile = "en-GB.dic"
            };
            _dictionary.Initialize();
            // Set the spell checker with a dictionary.
            _spellChecker = new Spelling
            {
                Dictionary = _dictionary
            };
            // Initialize the anagrams endpoint and pass in the api helper.
            _anagramsEndpoint = new AnagramsEndpoint(new APIHelper());
        }
        public Dictionary(WordDictionary dictionary, Spelling spellChecker, IAnagramsEndpoint anagramsEndpoint)
        {
            _dictionary = dictionary;
            _spellChecker = spellChecker;
            _anagramsEndpoint = anagramsEndpoint;
            _dictionary.DictionaryFile = "en-US.dic";

            _dictionary.Initialize();
            _spellChecker.Dictionary = _dictionary;
        }
        public async Task<List<string>> GetAnagrams(string inputText)
        {

            Anagrams = await _anagramsEndpoint.GetAnagrams(inputText);
            return Anagrams;
        }
        public bool CheckSpelling(string word)
        {
            return _spellChecker.TestWord(word) || _anagrams.Contains(word);
        }

        IEnumerable<string> IDictionary.GetLocalAnagrams(string inputText)
        {
            throw new NotImplementedException();
        }
    }
}
