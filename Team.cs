using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall;

public class Team
{
    //data
    public List<Player> Players { get; } = new List<Player>(); // list of players
    public string Name { get; private set; } // team name
    public Game Game { get; set; } // sets the game

    
    public Team(string name) // team constructor
    {
        Name = name;
    }

    public void StartGame(int width, int height) // let's start the game with width and height
    {
        Random rnd = new Random();
        foreach (var player in Players) // sets random position for each player
        {
            player.SetPosition
                (
                rnd.NextDouble() * width,
                rnd.NextDouble() * height
                );
        }
    }

    public void AddPlayer(Player player) // we add a player
    {
        if (player.Team != null) return;
        Players.Add(player);
        player.Team = this;
    }

    public (double, double) GetBallPosition() // we get the balls position
    {
        return Game.GetBallPositionForTeam(this);
    }

    public void SetBallSpeed(double vx, double vy) // set the speed of the ball
    {
        Game.SetBallSpeedForTeam(this, vx, vy);
    }

    public Player GetClosestPlayerToBall() // Get the closest player to the ball
    {
        Player closestPlayer = Players[0];
        double bestDistance = Double.MaxValue;
        foreach (var player in Players) //gets and returns the closest player to a ball
        {
            var distance = player.GetDistanceToBall();
            if (distance < bestDistance)
            {
                closestPlayer = player;
                bestDistance = distance;
            }
        }

        return closestPlayer;
    }

    public void Move() // moving towards the ball
    {
        GetClosestPlayerToBall().MoveTowardsBall();
        Players.ForEach(player => player.Move());
    }
}
