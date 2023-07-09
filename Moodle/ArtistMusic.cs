using Server.Moodle.DAL;

namespace Server.Moodle
{
    public class ArtistMusic
    {

        public string ArtistName { get; private set; }
        public string Likes { get; private set; }
        public ArtistMusic(string artistName, string likes)
        {
            ArtistName = artistName;
            Likes = likes;
        }
        public static bool AddFavoriteArtist(string UserId, string ArtistName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.AddFavoriteArtist(UserId, ArtistName);
        }
        public static bool DeleteFavoriteArtist(string UserId, string ArtistName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.DeleteFavoriteArtist(UserId, ArtistName);
        }
        public static List<ArtistMusic> GetFavoriteArtistByUserId(string UserId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetFavoriteArtistByUserId(UserId);
        }
        public static int GetTheNumberOfAppearanceInUserByGivenArtist(string ArtistName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTheNumberOfAppearanceInUserByGivenArtist(ArtistName);
        }
        public static int GetNumberOfPlayedForGivenArtist(string ArtistName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetNumberOfPlayedForGivenArtist(ArtistName);
        }

    }
}
