using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall;

public class Player
{
    //data
    public string Name { get; } //player name
    public char Symbol { get; } // Get the players symbol
    public double X { get; private set; } //horizontal position
    public double Y { get; private set; } //vertical position
    
    private double _vx, _vy; // distance between player and ball
    public Team? Team { get; set; } = null; // the team the player plays for

    private const double MaxSpeed = 1; //  Max player speed
    private const double MaxKickSpeed = 25; // Max kick speed 25
    private const double BallKickDistance = 10; // kick distance

    private Random _random = new Random(); // random number       

    //konstruktorid
    public Player(string name)  // sets the name of a player
    {
        Name = name;
    }
    public Player(char symbol) // sets the symbol of a player
    {
        Symbol = symbol;
    }

    public Player(string name, double x, double y, Team team) //the player's info, his name, pos x and y and the team he plays for
    {
        Name = name;
        X = x-3;
        Y = y-3;
        Team = team;
    }

    public void SetPosition(double x, double y) //set position x and y
    {
        X = x;
        Y = y;
    }

    public (double, double) GetAbsolutePosition()  // Position in which a player plays depending on the team
    {
        return Team!.Game.GetPositionForTeam(Team, X, Y);
    }

    public double GetDistanceToBall() // distance to the ball
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public void MoveTowardsBall() // move directly towards the ball
    {
        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
        _vx = dx / ratio;
        _vy = dy / ratio;
    }

    public void Move() // movement
    {
        if (Team.GetClosestPlayerToBall() != this)  //Team closest to the ball
        {
            _vx = 0;
            _vy = 0;
        }

        if (GetDistanceToBall() < BallKickDistance) //Calculate the ball kick speed
        {
            Team.SetBallSpeed(
                MaxKickSpeed * _random.NextDouble(),
                MaxKickSpeed * (_random.NextDouble() - 0.5)
                );
        }

        var newX = X + _vx;
        var newY = Y + _vy;
        var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY); //we define new position x and y
        if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
        {
            X = newX;
            Y = newY;
        }
        else
        {
            _vx = _vy = 0;
        }
    }

    public void Draw() // Let's draw a player with our symbol
    {
        Console.SetCursorPosition((int)Math.Round(X), (int)Math.Round(Y));
        Console.Write(Symbol); 
    }
}
