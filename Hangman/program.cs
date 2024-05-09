using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    class Program
    {
        // lista för rätta bokstäver
        static List<char> guessedLetters = new List<char>();

        // listan för misslyckade bokstäver
        static List<char> failedGuesses = new List<char>();

        static void Main(string[] args)
        {
            ////// Meny loop //////
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        static bool MainMenu()
        {
            ////// Menu ////////
            Console.WriteLine(" O");
            Console.WriteLine("/|\\");
            Console.WriteLine("/\\");
            Console.WriteLine("");
            Console.WriteLine("Hangman Game");
            Console.WriteLine("");
            Console.WriteLine("Press 1 for animal genre");
            //Console.WriteLine("Press 2 for fruit genre");
            //Console.WriteLine("Press 3 for color genre");
            Console.WriteLine("");
            Console.WriteLine("Press 9 for exit");
            Console.WriteLine("");
            Console.Write("\r\nSelect an option: ");

            // Keypress
            switch (Console.ReadLine())
            {
                case "1":
                    AnimalGenre();
                    return true;
                ///////////// fler genres ifall jag har tid /////////// nopes <<
                //case "2":
                //    fruitString();
                //    return true;
                //case "3":
                //    colorString();
                //    return true;
                case "9":
                    return false;
                default:
                    return true;
            }
        }

        static void AnimalGenre()
        {
            //slumpar ett ord från GenresCollection  
            //"$" för att gömma allt inom {}
            string[] animalWords = GenresCollection.animalWords;
            Random rand = new Random();
            int animalIndex = rand.Next(0, animalWords.Length);
            string randomAnimal = animalWords[animalIndex];
            Console.WriteLine($"random animal name: {randomAnimal}");
            Console.WriteLine($"Length of word: {randomAnimal.Length}");

            Console.Clear();
            Console.Write("You have chosen the 'animal' genre press anywhere to continue");
            Console.ReadKey();
            Console.Clear();

            // gissningarna
            int attempts = 0;
            bool showMenu = true;
            while (showMenu)
            {
                if (attempts >= 10)
                {
                    Console.WriteLine("You have exceeded the maximum number of attempts.");
                    break;
                }

                showMenu = GuessMenu(randomAnimal);
                attempts++;
            }
        }

        static bool GuessMenu(string randomAnimal)
        {
            Console.WriteLine($"Attempts left: {10 - failedGuesses.Count}");

            // visar bokstäver du gissade fel på
            if (failedGuesses.Count > 0)
            {
                Console.WriteLine("Failed guesses:");
                foreach (char failedGuess in failedGuesses)
                {
                    Console.Write(failedGuess + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Guess a letter or enter the whole word: ");
            string guess = Console.ReadLine();

            if (guess.Length == 1)
            {
                char letter = guess[0];
                Console.WriteLine("You entered the letter: " + letter);

                // spara gissade bokstäver
                SaveGuessedLetter(letter, randomAnimal);

                //gömmer ordet
                string hiddenWord = HideWord(randomAnimal);
                Console.WriteLine($"Hidden word: {hiddenWord}");

                // kollar om ordet är gissat
                if (hiddenWord == randomAnimal)
                {
                    Console.WriteLine("Congratulations! You guessed the word!");
                    return false;
                }
            }
            else
            {
                if (guess.ToLower() == randomAnimal.ToLower())
                {
                    Console.WriteLine("Congratulations! You guessed the word!");
                    return false; 
                }
                else
                {
                    Console.WriteLine("Incorrect guess!");
                    failedGuesses.AddRange(guess.ToLower().Except(guessedLetters));
                }
            }

            return true;
        }

        // spara gissade bokstäver
        static void SaveGuessedLetter(char letter, string word)
        {
            guessedLetters.Add(letter);

            // kollar om den gissade bokstaven inte är ordet
            if (!word.Contains(letter))
            {
                failedGuesses.Add(letter);
            }
        }

        //byter ut ordet till "_"
        static string HideWord(string word)
        {
            string hiddenWord = "";
            foreach (char letter in word)
            {
                if (char.IsLetter(letter))
                {
                    // Kollar om du gissat bokstaven
                    if (guessedLetters.Contains(char.ToLower(letter)))
                    {
                        hiddenWord += letter;
                    }
                    else
                    {
                        hiddenWord += "_";
                    }
                }
                else
                {
                    hiddenWord += letter;
                }
            }
            return hiddenWord;
        }
    }
}
