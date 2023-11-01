using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall;

public class Game
{
    //data
    public Team TeamRed { get; } 
    public Team TeamBlue { get; }
    public Stadium Stadium { get; }
    public Ball Ball { get; private set; }
    public bool IsRunning { get; private set; } = true;
    public int ScoreB { get; private set; }
    public int ScoreR { get; private set; }

    //constructor
    public Game(Team redTeam, Team blueTeam, Stadium stadium)
    {
        TeamRed = redTeam;
        redTeam.Game = this;
        TeamBlue = blueTeam;
        blueTeam.Game = this;
        Stadium = stadium;
    }

    //We make a stop if the game is not playing
    public void Stop()
    {
        IsRunning = false;
    }

    public void Start() // starts the game, puts the ball in the middle, teams on their sides
    {
        Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
        TeamRed.StartGame(Stadium.Width / 2, Stadium.Height);
        TeamBlue.StartGame(Stadium.Width / 2, Stadium.Height);
    }
    private (double, double) GetPositionForTeamBlue(double x, double y) // Position for team red players
    {
        return (Stadium.Width - x, Stadium.Height - y);
    }

    public (double, double) GetPositionForTeam(Team team, double x, double y) // Position for team blue players
    {
        return team == TeamRed ? (x, y) : GetPositionForTeamBlue(x, y);
    }

    public (double, double) GetBallPositionForTeam(Team team) // Position for the ball
    {
        return GetPositionForTeam(team, Ball.X, Ball.Y);
    }

    public void SetBallSpeedForTeam(Team team, double vx, double vy)  // sets the ball direction when kicked that depends on a team that kicked it
    {
        if (team == TeamRed)//if kicked by team red it will go towards the side where blue team is
        {
            Ball.SetSpeed(vx, vy);
        }
        else//if kicked by team blue(or any other team) it will go towards the side where red team is
        {
            Ball.SetSpeed(-vx, -vy);
        }
    }

    public void Move() //movement, for both teams and the ball
    {
        TeamRed.Move();
        TeamBlue.Move();
        Ball.Move();

        if (Ball.Y <= 3) // if the position of the ball in height is less or equals to 3 then the red teams scores
        {
            ScoreR++; // red team score
            Console.Clear();
            Console.WriteLine($"Red Team: {ScoreR} Blue Team: {ScoreB}");
            Thread.Sleep(1000);
            
        }
        else if (Ball.Y >= Stadium.Height - 3) // if the position of the ball in height is more or equals to stadium height - 3 then the blue teams scores
        {
            ScoreB++; // blue team score
            Console.Clear();
            Console.WriteLine($"Red Team: {ScoreR} Blue Team: {ScoreB}");
            Thread.Sleep(1000);
            
        }
    }
}
