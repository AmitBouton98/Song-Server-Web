using Server.Moodle.DAL;

namespace Server.Moodle
{
    public class SongComment : Comment
    {
        public string SongId { get; set; }
        public SongComment(string userId, string id, string text, DateTime createDate, string songId, string stars) : base(userId, id, text, createDate, stars)
        {
            SongId = songId;
        }

        public static bool InsertSongCommentOrUpdate(SongComment songComment)
        {
            DBservices dBservices = new DBservices();
            return dBservices.InsertSongCommentOrUpdate(songComment);
        }
        public static List<SongComment> GetCommentBySongID(string SongId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAllSongComments(SongId);
        }
        public static double GetAvgNumberForGivenSong(string SongId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAvgNumberForGivenSong(SongId);
        }
        public static bool Delete(string CommentId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.DeleteSongComment(CommentId);
        }
    }
}
