using Server.Moodle.DAL;

namespace Server.Moodle
{
    public class SongComment : Comment
    {
        public string SongId { get; set; }  
        public SongComment(string userId, string id, string text, string createDate, string songId) : base(userId, id, text, createDate)
        {
            SongId = songId;    
        }
        public static bool InsertSongCommentOrUpdate(SongComment songComment)
        {
            DBservices dBservices = new DBservices();
            return dBservices.InsertSongCommentOrUpdate(songComment);
        }
        public static List<SongComment> GetCommentBySongID(string id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAllSongComments(id);
        }
        public static bool Delete(string id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.DeleteSongComment(id);
        }
    }
}
