using Caliburn.Micro;
using CountdownLettersWPF.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CountdownLettersWPF.ViewModels
{
    public class GameViewModel : Screen
    {
        //
        private readonly IDictionary _dictionary;
        private readonly ILetterGenerator _letterGenerator;
        private bool RoundHasStarted { get; set; } = false;
        private bool _gameHasStarted = false;

        public int VowelCount { get; set; } = 0; 
        public int ConsonantCount { get; set; } = 0;
        private string _status = string.Empty;
        private string _gameStatus = "Press Start";

        private int _score = 0;
        private int _round;


        private string _scrambledLetters = string.Empty;
        private string _userEntry = string.Empty;

        private List<string> _anagrams = new List<string>();


        /// <summary>
        /// The contructor thst uses di.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="letterGenerator"></param>
        public GameViewModel(IDictionary dictionary, ILetterGenerator letterGenerator)
        {
            _dictionary = dictionary;
            _letterGenerator = letterGenerator;
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

        }
        /// <summary>
        /// The public properties
        /// </summary>
        public bool GameHasStarted
        {
            get { return _gameHasStarted; }
            set
            {
                _gameHasStarted = value;
                NotifyOfPropertyChange(() => GameHasStarted);
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }
        public string GameStatus
        {
            get { return _gameStatus; }
            set
            {
                _gameStatus = value;
                NotifyOfPropertyChange(() => GameStatus);
            }
        }
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                NotifyOfPropertyChange(() => Score);
            }
        }
        public int Round
        {
            get { return _round; }
            set
            {
                _round = value;
                NotifyOfPropertyChange(() => Round);
            }
        }

        /// <summary>
        /// Public property Anagrams that also affects the AnagramString property
        /// </summary>
        public List<string> Anagrams
        {
            get { return _anagrams; }
            set 
            { 
                _anagrams = value;
                NotifyOfPropertyChange(() => Anagrams);
                NotifyOfPropertyChange(() => AnagramString);
            }
        }
        /// <summary>
        /// Public property AnagramString that is the formatted list of anagrams.
        /// </summary>
        public string AnagramString
        {
            get { return string.Join(" ", Anagrams.Where(a => a.Length > 3).Select(a => a).ToList()); }
        }
        /// <summary>
        /// Asynchronous method for getting all the anagrams for the scrambled letters.
        /// </summary>
        public async void GetAnagrams()
        {
            // The dictionary will get all the anagrams and store them.
            await _dictionary.GetAnagrams(ScrambledLetters);
            // Check that there are anagrams found.
            if (_dictionary.Anagrams.Count >= 1)
            {
                // Get only words that have 4 or more letters.
                _dictionary.Anagrams = _dictionary.Anagrams.Where(a => a.Length >= 4).ToList();
            }
            // Check that there are still anagrams in the list.
            if (_dictionary.Anagrams.Count < 1)
            {
                // No anagrams so restart the round.
                RestartRound();
                
            }
            else
            {
                // Update the status
                Status = $"Found {_dictionary.Anagrams.Count} anagrams for '{ScrambledLetters.ToUpper()}'";

                Status = $"Found {_dictionary.Anagrams.Count} anagrams - Unscramble your word.";
            }
        }
        public string ScrambledLetters
        {
            get { return _scrambledLetters; }
            set
            {
                _scrambledLetters = value;
                NotifyOfPropertyChange(() => ScrambledLetters);
                // Check if all the letters have been choosen
                if (ScrambledLetters.Length == 9)
                {
                    // Get the list of anagrams for the random string
                    Status = "Getting Anagrams List...";
                    var task = Task.Run(() => GetAnagrams());
                }
            }
        }
        public string UserEntry
        {
            get { return _userEntry; }
            set
            {
                // Validate the characters are letters.
                string letters = new string(value?.Where(c => char.IsLetter(c)).Select(c => c).ToArray());
                // Make sure the length is more than 9.
                if(letters?.Length > 9)
                {
                    // Get the first 9 letters.
                    letters = letters.Substring(0, 9);
                }
                string sLetters = ScrambledLetters;
                // Loop through and check the scrambled letters.
                foreach (char c in letters)
                {
                    // Check for the index
                    int loc = sLetters.IndexOf(c);
                    // Check if the index is found.
                    if (loc != -1)
                    {
                        // Remove the letter from the scrambled string.
                        sLetters = sLetters.Remove(loc, 1);
                    }
                    else
                    {
                        // The user has entered a invalid letter so exit the loop.
                        letters = null;
                        break;
                    }
                }
                // Only update if the valid entry has changed.
                if (letters != null && _userEntry != letters)
                {
                    _userEntry = letters;
                    NotifyOfPropertyChange(() => UserEntry);
                    NotifyOfPropertyChange(() => CanCheckWord);
                }

            }
        }
        /// <summary>
        /// The Add Consonent button enabled property.
        /// </summary>
        public bool CanAddConsonant
        {
            get
            {
                return (GameHasStarted && RoundHasStarted && (ConsonantCount < 6) && (ConsonantCount + VowelCount < 9));
            }

        }
        public void AddConsonant()
        {
            ScrambledLetters += _letterGenerator.GetConsonant();
            ConsonantCount += 1;
            NotifyOfPropertyChange(() => CanAddConsonant);
            NotifyOfPropertyChange(() => CanAddVowel);
            NotifyOfPropertyChange(() => CanCheckWord);
        }
        /// <summary>
        /// The Add Vowel button enabled property.
        /// </summary>
        public bool CanAddVowel
        {
            get
            {
                return (GameHasStarted && RoundHasStarted && (VowelCount < 5) && (ConsonantCount + VowelCount < 9));
            }

        }
        public void AddVowel()
        {
            ScrambledLetters += _letterGenerator.GetVowel();
            VowelCount += 1;
            NotifyOfPropertyChange(() => CanAddConsonant);
            NotifyOfPropertyChange(() => CanAddVowel);
            NotifyOfPropertyChange(() => CanCheckWord);
        }
        /// <summary>
        /// The Check Word button enabled property.
        /// </summary>
        public bool CanCheckWord
        {
            get
            {
                return (GameHasStarted && RoundHasStarted && (Round < 5) && (ScrambledLetters.Length > 3) && UserEntry.Length > 3);
            }

        }
        public void CheckWord()
        {
            // Check if the user input is valid.
            if (CheckValidEntry())
            {
                Score += UserEntry.Length;
                Status = $"Your choice was found. You scored {UserEntry.Length} points.";
                Anagrams = _dictionary.Anagrams.Where(a => a.Length >= UserEntry.Length).ToList();
            }
            else
            {
                Status = "No matches found!";
                Anagrams = _dictionary.Anagrams.Where(a => a.Length >= 4).ToList();
            }
            
            
            // Check if we are at the last round.
            if(Round == 4)
            {
                GameHasStarted = false;
                GameStatus = $"Game Finished {Score} Points";
            }
            RoundHasStarted = false;
            NotifyOfPropertyChange(() => CanCheckWord);
            NotifyOfPropertyChange(() => CanStartNextRound);

        }
        public bool CheckValidEntry()
        {
            return _dictionary.CheckSpelling(UserEntry) || _dictionary.Anagrams.Contains(UserEntry);
        } 
        
        /// <summary>
        /// 
        /// </summary>
        public void StartNewGame()
        {
            GameStatus = "Game Started";
            Score = 0;
            Round = 0;
            GameHasStarted = true;
            StartNextRound();
        }
        /// <summary>
        /// The Start Next Round button enabled property.
        /// </summary>
        public bool CanStartNextRound
        {
            get
            {
                return (GameHasStarted && !RoundHasStarted && Round <= 3);
            }

        }
        /// <summary>
        /// Starts the next round.
        /// </summary>
        public void StartNextRound()
        {
            // Set the satus for the new round.
            Status = "Select between 3 - 5 vowels or 4 - 6 consonants.";
            // Clear the raound data.
            ClearRound();
            // Increment the round count.
            Round++;
            // Set the round has started flag.
            RoundHasStarted = true;
            // Notify properties have changed.
            NotifyOfPropertyChange(() => CanAddConsonant);
            NotifyOfPropertyChange(() => CanAddVowel);
            NotifyOfPropertyChange(() => CanStartNextRound);
        }
        /// <summary>
        /// Restarts the round.
        /// </summary>
        public void RestartRound()
        {
            Status = "No anagrams found! Round has restarted.";
            ClearRound();
        }
        /// <summary>
        /// Clears the round data.
        /// </summary>
        public void ClearRound()
        {
            ScrambledLetters = string.Empty;
            UserEntry = string.Empty;
            
            ConsonantCount = 0;
            VowelCount = 0;

            _dictionary.Anagrams = new List<string>();
            Anagrams = new List<string>();
        }
    }
}
