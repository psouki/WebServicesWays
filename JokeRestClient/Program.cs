using System;
using System.Collections.Generic;

namespace JokeRestClient
{
    class Program
    {
        private static JokeDataService _service;
        static void Main(string[] args)
        {
            _service = new JokeDataService();
            string command = string.Empty;
            while (command != "4")
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("------------------");
                Console.WriteLine("1) List jokes");
                Console.WriteLine("2) Choose a joke");
                Console.WriteLine("3) Create a new joke");
                Console.WriteLine("4) Quit");

                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        ListJokes();
                        break;
                    case "2":
                        ShowJoke();
                        break;
                    case "3":
                        EnterJoke();
                        break;
                }
                Console.Clear();
            }
        }

        private static void EnterJoke(Joke joke = null)
        {
            if (joke == null)
            {
                joke = new Joke();
            }

            Console.WriteLine("The joke's title:");
            joke.Title = Console.ReadLine();

            Console.WriteLine("Question:");
            joke.Question = Console.ReadLine();

            Console.WriteLine("Answer:");
            joke.Answer = Console.ReadLine();

            Console.WriteLine("Grade:");
            int grade;
            int.TryParse(Console.ReadLine(), out grade);
            joke.Grade = grade;

            joke = joke.Id != 0 ? _service.UpdateJoke(joke) : _service.AddJoke(joke);

            Console.WriteLine("------------");
            Console.WriteLine("Id " + joke.Id);
            Console.WriteLine("Title " + joke.Title);
            Console.WriteLine("Question: " + joke.Question);
            Console.WriteLine("Answer: " + joke.Answer);
            Console.WriteLine("Grade: " + joke.Grade);
            joke.Answer = Console.ReadLine();

        }

        private static void ShowJoke()
        {
            Console.WriteLine("Number?");
            string index = Console.ReadLine();
            int jokeId;
            bool isNumber = int.TryParse(index, out jokeId);
            if (isNumber)
            {
                Joke joke = _service.GetJokeById(jokeId);
                if (joke != null)
                {
                    Console.WriteLine($"({joke.Id}) - {joke.Title}");
                    Console.WriteLine($"{joke.Question}");
                    Console.WriteLine($"{joke.Answer}");
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("Please enter a command: ");
                    Console.WriteLine("1) Edit");
                    Console.WriteLine("2) Delete");
                    Console.WriteLine("Any key to return to menu");
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "1":
                            EnterJoke(joke);
                            break;
                        case "2":
                            DeleteJoke(joke.Id);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Joke not found");
                }
            }
            else
            {
                Console.WriteLine("Joke not found");
            }
        }

        private static void DeleteJoke(int id)
        {
            _service.DeleteJoke(id);
        }

        private static void ListJokes()
        {
            ICollection<Joke> jokes = _service.GetJokes();
            foreach (Joke joke in jokes)
            {
                Console.WriteLine($"({joke.Id}) - {joke.Title}");
            }
            Console.WriteLine("---------------");
            Console.WriteLine("Any key to return to menu");
            Console.ReadLine();
        }
    }

}
