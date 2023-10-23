using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;
using System.Runtime.CompilerServices;

namespace MyProject;
class Program
{
    // Some details to fix about the game:

    // How to add the possibility to guess the whole word instead of giving letters one by one?

    // Is it possible to make different themes around the game, randomize them
    // and then let the user know current game's theme, once the game starts?

    // Close game command? Now the game will only close after either completion or game over. 


    // Here we have a method for printing out the right version of hanged man,
    // depending how many times the user has guessed wrong already.
    private static void PrintHangman(int wrong)
    {
        if (wrong == 0)
        {
            Console.WriteLine("\n+---+)");
            Console.WriteLine("    |");
            Console.WriteLine("    |");
            Console.WriteLine("    |");
            Console.WriteLine("   ===");
        }
        else if (wrong == 1)
        {
            Console.WriteLine("\n+---+)");
            Console.WriteLine(" o  |");
            Console.WriteLine("    |");
            Console.WriteLine("    |");
            Console.WriteLine("   ===");
        }
        else if (wrong == 2)
        {
            Console.WriteLine("\n+---+)");
            Console.WriteLine(" o  |");
            Console.WriteLine(" |  |");
            Console.WriteLine("    |");
            Console.WriteLine("   ===");

        }
        else if (wrong == 3)
        {
            Console.WriteLine("\n+---+)");
            Console.WriteLine(" o  |");
            Console.WriteLine("/|  |");
            Console.WriteLine("    |");
            Console.WriteLine("   ===");
        }
        else if (wrong == 4)
        {
            Console.WriteLine("\n+---+)");
            Console.WriteLine(" o  |");
            Console.WriteLine("/|\\ |");
            Console.WriteLine("    |");
            Console.WriteLine("   ===");

        }
        else if (wrong == 5)
        {
            Console.WriteLine("\n+---+)");
            Console.WriteLine(" o  |");
            Console.WriteLine("/|\\ |");
            Console.WriteLine("/   |");
            Console.WriteLine("   ===");
        }
        else if (wrong == 6)
        // Note, that this is the last possible wrong answer. Once wrong = 6, game is over. 
        {
            Console.WriteLine("\n+---+)");
            Console.WriteLine(" o  |");
            Console.WriteLine("/|\\ |");
            Console.WriteLine("/ \\ |");
            Console.WriteLine("   ===");

            Console.WriteLine("Olet joutunut hirteen, pelisi on päättynyt.");
        }
    }

    // Here we have a method for printing out the word. 
    private static int PrintWord(List<char>guessedLetters, string randomWord)
    {
        int counter = 0;
        int rightLetters = 0;
        Console.Write("\r\n");
        foreach (char c in randomWord)
        {
            if (guessedLetters.Contains(c))
            {
                Console.Write(c + " ");
                rightLetters++;
            }
            else
            {
                Console.Write(" ");
            }
            counter++;
        }
        return rightLetters;
    }

    // And here is a method for printing out lines underneath the randomly generated word. 
    private static void PrintLines(string randomWord)
    {
        Console.Write("\r");
        foreach(char c in randomWord)
        {
            // This will print out the lines under the letters. Currently works so-so, still needs some fixing!
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.Write("\u0305 ");
        }
    }

    // And here is the actual playing part of the game. 
    static void Main(string[] args)
    {
        // current theme of the game is photography,
        // if you need to change it, just change the string + print name and edit the list below. 
        string photographyDictionary = "valokuvaus";
        // Giving the user a warm welcome to play this game:
        Console.WriteLine("Tervetuloa pelaamaan hirsipuuta!");
        Console.WriteLine(("Sanat ovat aiheesta " + photographyDictionary + "."));
        Console.WriteLine("------------------------");

        // Here we generate a random word out of the given possibilites from a list:
        Random random = new Random();
        // Test, if it's able to import the dictionary as a separate .txt file with ReadFile command. 
        // If it works, try to create different themes for the game using those files. 
        List<string> dictionary = new List<string> { "ASYMMETRINEN ", "VALOTUSAUKKO", "KENNO", "OBJEKTIIVI", "SALAMAVALO", "JALUSTA", "TAUSTAKANGAS", "PIMIÖ", "FILMIRULLA", "MUSTAVALKOKUVA", "ALBUMI", "PORTFOLIO", "DIGIKUVA", "POLARISAATIO", "SUOJALINSSI", "VALOKUVAAJA", "LISÄVALO", "KUVANKÄSITTELY", "SEEPIA", "TARKENNUS", "KAMERALAUKKU", "ITSELAUKAISIN" };
        int index = random.Next(dictionary.Count);
        string randomWord = dictionary[index];

        // And here the lines are printed just under the random word, so player knows how long is the word.
        foreach (char x in randomWord)
        {
            Console.Write("_ ");
        }

        // Here we define it to the program as well, 
        // + starting from 0 guesses, 0 rights and making a list to storing guesses. 
        int lenghtOfTheWordToGuess = randomWord.Length;
        int amountOfTimesWrong = 0;
        List<char> currentLettersGuessed = new List<char>();
        int currentLettersRight = 0;

        // As long as we're inside the while loop, the game continues. 
        while (amountOfTimesWrong != 6 && currentLettersRight != lenghtOfTheWordToGuess)
        {
            // This prints out the already guessed letters. 
            Console.Write("\nOlet jo arvannut kirjaimet: ");
            foreach (char letter in currentLettersGuessed)
            {
                Console.Write(letter + " ");
            }

            // Here we ask the user to guess a letter:
            Console.Write("\nArvaa kirjain: ");
            // Every letter the player guesses, it will be printed & handled as uppercase. 
            char letterGuessed = char.ToUpper(Console.ReadLine()[0]);
            if (currentLettersGuessed.Contains(letterGuessed))
            {
                // If it has been guessed, the user is told so. 
                Console.Write("\r\nOlet jo arvannut tätä kirjainta.");
                // Show the user their progress in the form of a hanged man:
                PrintHangman(amountOfTimesWrong);
                // Print the current, rightly guessed letters and the amount of lines between:
                currentLettersRight = PrintWord(currentLettersGuessed, randomWord);
                PrintLines(randomWord);
            }
            else
            {
                // By default, the answer is false.
                bool right = false;
                // Once a guess has been made, this loop goes through the whole random word and searches if the letter can be found.
                for (int i = 0; i < randomWord.Length; i++)
                    // If yes, bool right is changed to true:
                    if (letterGuessed == randomWord[i]) { right = true; }
                    if (right) 
                    // Let's add the correct letter to the showing letters and print the progress + hanged man so far. 
                    {
                        PrintHangman(amountOfTimesWrong);
                        currentLettersGuessed.Add(letterGuessed);
                        currentLettersRight = PrintWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        PrintLines(randomWord);
                    }
                    else
                    // If the guess is wrong, let's add amount of times wrong -> changing the graphic of hanged man. 
                    // Also add to guessed letters and print out the progress again. 
                    {
                        amountOfTimesWrong++;
                        currentLettersGuessed.Add(letterGuessed);
                        PrintHangman(amountOfTimesWrong);
                        currentLettersRight = PrintWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        PrintLines(randomWord);
                    }
                }
            }
            // And here, outside the while loop (= after the game), we'll thank the user for playing. 
            Console.WriteLine("\r\nKiitos, että pelasit hirsipuuta!");
    }
}