using QueryBuilder;
using System.Reflection.Emit;
using System.Windows.Markup;

namespace QueryBuilder
{
    public class Driver
    {
        static public void Main(string[] args)
        {
            string FilePath = "../../../Data/data.db";
            string PokePath = "../../../AllPokemon.csv";
            string GamePath = "../../../BannedGames.csv";
            QueryBuilder QBuilder = new QueryBuilder(FilePath);

            var PokeReader = new StreamReader(PokePath);
            var GameReader = new StreamReader(GamePath);


                QBuilder.DeleteAll<Pokemon>();
                Console.WriteLine("All Pokemon have been deleted from the database.");
                QBuilder.DeleteAll<BannedGame>();
                Console.WriteLine("All Banned Games have been deleted from the database.");

            //adds all pokemon to database
            while (!PokeReader.EndOfStream)
                {
                    var line = PokeReader.ReadLine();
                    var attributes = line.Split(',');

                    if (attributes.Length == 13)
                    {
                    Pokemon pokemon = new Pokemon
                    {
                        DexNumber = int.Parse(attributes[0]),
                        Name = attributes[1],
                        Form = attributes[2],
                        Type1 = attributes[3],
                        Type2 = attributes[4],
                        Total = int.Parse(attributes[5]),
                        HP = int.Parse(attributes[6]),
                        Attack = int.Parse(attributes[7]),
                        Defense = int.Parse(attributes[8]),
                        SpecialAttack = int.Parse(attributes[9]),
                        SpecialDefense = int.Parse(attributes[10]),
                        Speed = int.Parse(attributes[11]),
                        Generation = int.Parse(attributes[12]),

                    };
                    QBuilder.Create(pokemon);
                    Console.WriteLine("Inserted Pokemon: " + pokemon.Name);
                    }
                }

            //adds all banned games to database
            while (!GameReader.EndOfStream)
            {
                var line = GameReader.ReadLine();
                var attributes = line.Split(',');

                if (attributes.Length == 4)
                {
                    BannedGame game = new BannedGame
                    {
                        Title = attributes[0],
                        Series = attributes[1],
                        Country = attributes[2],
                        Details = attributes[3],

                    };
                    QBuilder.Create(game);
                    Console.WriteLine("Inserted Banned Game: " + game.Title);
                }
            }

            //adds singular pokemon
            Pokemon NewPokemon = new Pokemon()
            {
                DexNumber = 899,
                Name = "Gyrados",
                Type1 = "Water",
                Type2 = "Flying",
                Total = 540,
                HP = 95,
                Attack = 125,
                Defense = 79,
                SpecialAttack = 60,
                SpecialDefense = 100,
                Speed = 81,
                Generation = 1,

            };
            QBuilder.Create(NewPokemon);
            Console.WriteLine("Inserted Pokemon: " + NewPokemon.Name);

            //adds singular banned game
            BannedGame NewGame = new BannedGame()
            {
                Title = "Splatoon",
                Series = "Splatoon",
                Country = "United States",
                Details = "Banned due to the game unfairly inking on players and therefore a second one had to be made."

            };
            QBuilder.Create(NewGame);
            Console.WriteLine("Inserted Pokemon: " + NewGame.Title);
        
    }
    }
}