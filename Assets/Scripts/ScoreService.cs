public class ScoreService : IScoreService
{
    public int LeftScore { get; private set; }
    public int RightScore { get; private set; }

    public void AddGoal(Team team)
    {
        switch (team)
        {
            case Team.Left:
                LeftScore++;
                break;

            case Team.Right:
                RightScore++;
                break;
        }
    }
}