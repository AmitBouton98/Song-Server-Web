using Server.Moodle.DAL;

namespace Server.Moodle
{
    public class UserPoint
    {
        public UserPoint(string userId, string score)
        {
            UserId = userId;
            Score = score;
        }

        public string UserId { get; set; }
        public string Score { get; set; }
        public static bool CreateOrUpdateScore(UserPoint up)
        {
            DBservices dBservices = new DBservices();
            return dBservices.CreateOrUpdateScore(up);
        }
        public static List<UserPoint> GetTop10Scores()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTop10Scores();
        }
    }
}
