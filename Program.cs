using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace src
{
    internal class Program
    {
        private static int[] coordinates = new int[2];
        static void Main(string[] args)
        {

            GuessingGrid g = new GuessingGrid(10, 10);
            ShipGrid battleField = new ShipGrid(10, 10);

            List<Ship> myTroops = new List<Ship>()
            {
            new Ship(3), new Ship(4), new Ship(5)
            };

            bool isOutOfBounds = true;

            //boucle pour placer les bateau
            // i the ship position
            for (int i = 0; i < myTroops.Count; i++)
            {
                do
                {
                    Console.WriteLine("Please enter the position of ship n°" + (i + 1) + ". It takes " + myTroops[i].length + " square(s)");
                    string position = Console.ReadLine().ToLower();
                    if (position.Length < 2)
                    {
                        Console.WriteLine("Incorrect format");
                        continue;
                    }
                    int firstCoord = position[0] - 'a', secondCoord = int.Parse(position.Substring(1)) - 1;
                    Console.WriteLine("In what direction should it lay (up/down/left/right) ?");
                    string direction = Console.ReadLine().ToLower();
                    isOutOfBounds = !battleField.AddShip(myTroops[i], firstCoord, secondCoord, direction);

                } while (isOutOfBounds);


            }

            battleField.Display();

            Client client = new Client();
            client.ConnectServer();

            //Client loop

            int serverShips = 3;

            while (serverShips > 0 && myTroops.Count > 0)
            {
                //Au tour du client, on affiche la grille de l'ennemi(e)
                g.Display();

                int x = 0, y = 0;
                string input = "";
                bool quit = false;

                Console.WriteLine("Where do you want to fire (from A1 to J10) ?");
                while (!quit)
                {
                    quit = true;
                    input = Console.ReadLine().ToLower();
                    if (input.Length < 2)
                    {
                        Console.WriteLine("Incorrect input, at least 2 characters try again (from A1 to J10)");
                        quit = false;
                    }

                    x = input[0] - 'a';
                    y = int.Parse(input.Substring(1)) - 1;
                    if (x < 0 || y < 0 || x > 9 || y > 9)
                    {
                        Console.WriteLine("Input out of bounds, try again (from A1 to J10) ");
                        quit = false;
                    }


                }

                client.SendResponse(input);
                string response = client.GetResponse();
                Console.WriteLine(response);
                switch (response)
                {
                    case "missed":
                        {
                            g.ChangeState(x, y, false);
                            Console.WriteLine("You missed !!");
                            break;

                        }
                    case "hit":
                        {
                            g.ChangeState(x, y, true);
                            Console.WriteLine("You hit !! Well played");
                            break;
                        }

                    case "sunk":
                        {
                            g.ChangeState(x, y, true);
                            Console.WriteLine("You hit and sunk their ship !! Congratz");
                            serverShips--;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("The client is a troll");
                            return;
                        }
                }

                string enemyChoice = client.GetPosition(coordinates);
                if (enemyChoice == "")
                    break;
                Console.WriteLine("The ennemy is firing at " + enemyChoice);
                Ship s = battleField.grid[coordinates[1], coordinates[0]];
                if (s != null)
                {
                    battleField.grid[coordinates[1], coordinates[0]] = null;
                    s.health--;
                    battleField.Display();
                    if (s.health == 0)
                    {
                        Console.WriteLine("... and BOOM you got hit !");
                        client.SendResponse("sunk");
                        myTroops.Remove(s);
                        Console.WriteLine("your ship got destroyed :'(");
                        Console.WriteLine("you have " + myTroops.Count + " ship(s) left");

                    }
                    else
                    {
                        Console.WriteLine("... and BOOM you got hit !");
                        client.SendResponse("hit");
                    }

                }

                else
                {
                    battleField.Display();
                    Console.WriteLine("... and he missed ! What a loser");
                    client.SendResponse("missed");
                }

                if (myTroops.Count == 0)
                {
                    Console.WriteLine("Game over, you lost");
                    break;
                }

                
                

            }

            

        }

    }
}