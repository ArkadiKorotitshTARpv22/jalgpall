using Jalgpall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Jalgpall
{
    class Program
    {
        static void Main(string[] args)
        {
            // team score
            Console.WriteLine("Red Team: 0 Blue Team: 0");

            //we create 2 teams
            Team t1 = new Team("Esimene");
            Team t2 = new Team("Teine");

            //size of a stadium and console window
            int mapWidth = 60;
            int mapHeight = 60;

            //console window size
            Console.SetWindowSize(mapWidth, mapHeight);


            // Stadium            
            Stadium s = new Stadium(mapWidth - 2, mapHeight - 2);

            //make a cycle and add players to the team
            for (int i = 1; i <= 22; i++)
            {
                Player player = new Player($"Player {i}");

                if (i <= 11)
                {
                    t1.AddPlayer(player);
                }
                else
                {
                    t2.AddPlayer(player);
                }
            }

            // we create new game
            Game g = new Game(t1, t2, s);

            // ball in the centre
            Ball ball = new Ball(mapWidth / 2, mapHeight / 2, g);

            // Start the game
            g.Start();

            while (true)
            {
                //let's make the game move
                g.Move();

                //draw players and ball
                DrawField(s.Width, s.Height, t1.Players, t2.Players, g.Ball);

                // we draw a stadium
                s.Draw();

                //if the game does not play then the game ends and writes game over
                if (!g.IsRunning)
                {
                    Console.WriteLine("Game Over!");
                    break;
                }

                //wait
                Thread.Sleep(1000);
            }
        }
        private static void DrawField(int width, int height, List<Player> t1, List<Player> t2, Ball ball) //draw the players and the ball, colour the players to differentiate them
        {
            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(3, y + 1);

                for (int x = 0; x < width; x++)
                {
                    if (IsPlayerAtPosition(x, y, t1))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("R");
                        Console.ResetColor();
                    }
                    else if (IsPlayerAtPosition(x, y, t2))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("B");
                        Console.ResetColor();
                    }
                    else if (IsBallAtPosition(x, y, ball))
                    {
                        ball.Draw();
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
            }
        }

        private static bool IsPlayerAtPosition(int x, int y, List<Player> players) //if the player's pos x and pos y are correct we return true, but if not we return false
        {
            foreach (var player in players)
            {
                int playerX = (int)Math.Round(player.X);
                int playerY = (int)Math.Round(player.Y);

                if (playerX == x && playerY == y)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsBallAtPosition(int x, int y, Ball ball) //we find out if the ball is present or not, we get the ball x and y pos and return it x and y pos.
        {
            int ballX = (int)Math.Round(ball.X);
            int ballY = (int)Math.Round(ball.Y);
            return ballX == x && ballY == y;
        }
    }
}