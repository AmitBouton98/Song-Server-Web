using Server.Moodle.DAL;

namespace Server.Moodle
{
    public class ArtistComment : Comment
    {
        public string ArtisName { get; set; }
        public ArtistComment(string userId, string id, string text, DateTime createDate, string artisName, string stars) : base(userId, id, text, createDate, stars)
        {
            ArtisName = artisName;
        }
        public static bool InsertArtistCommentOrUpdate(ArtistComment artistC)
        {
            DBservices dBservices = new DBservices();
            return dBservices.InsertArtistCommentOrUpdate(artistC);
        }
        public static List<ArtistComment> GetCommentByArtistName(string artistName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAllArtistComments(artistName);
        }
        public static double GetAvgNumberForGivenArtist(string artistName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAvgNumberForGivenArtist(artistName);
        }
        public static bool Delete(string id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.DeleteArtistComment(id);
        }

    }
}
