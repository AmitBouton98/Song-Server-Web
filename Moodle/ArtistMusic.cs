using Server.Moodle.DAL;

namespace Server.Moodle
{
    public class ArtistMusic
    {

        public string ArtistName { get; private set; }
        public string Likes { get; private set; }
        public string ArtistUrl { get; private set; }

        public ArtistMusic(string artistName, string likes, string artistUrl)
        {
            ArtistName = artistName;
            Likes = likes;
            ArtistUrl = artistUrl;
        }

        public static int GetNumberOfArtists()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetNumberOfArtists();
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
        public static bool UpdateArtistUrl(string ArtistName, string ArtistUrl)
        {
            DBservices dBservices = new DBservices();
            return dBservices.UpdateArtistUrl(ArtistName, ArtistUrl);
        }
        public static List<ArtistMusic> GetFavoriteArtistByUserId(string UserId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetFavoriteArtistByUserId(UserId);
        }
        public static List<ArtistMusic> GetTop10Artists()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTop10Artists();
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
        public static List<ArtistMusic> GetAllArtists()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAllArtists();
        }
        public static ArtistMusic GetArtistByName(string name)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetArtistByName(name);
        }
        public static bool InsertOrUpdateArtist(ArtistMusic artist)
        {
            DBservices dBservices = new DBservices();
            return dBservices.InsertArtistOrUpdate(artist);
        }
        public static bool Delete(string name)
        {
            DBservices dBservices = new DBservices();
            return dBservices.DeleteArtist(name);
        }
    }
}
