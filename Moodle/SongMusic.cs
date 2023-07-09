using Server.Moodle.DAL;

namespace Server.Moodle
{
    public class SongMusic
    {
        public string Id { get; private set; } 
        public string ArtistName { get; private set; }
        public string Name { get; private set; }
        public string Likes { get; private set; } 
        public string LyricLink { get; private set; }
        public string PlayLink { get; private set; }
        public SongMusic(string id, string artistName, string name, string likes, string lyricLink, string playLink)
        {
            Id = id;
            ArtistName = artistName;
            Name = name;
            Likes = likes;
            LyricLink = lyricLink;
            PlayLink = playLink;
        }
        public static bool AddFavoriteSong(string UserId, string SongId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.AddFavoriteSong(UserId, SongId);
        }
        public static bool CreateOrUpdateNumberOfPlayed(string SongId, string UserId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.CreateOrUpdateNumberOfPlayed(SongId, UserId);
        }
        public static bool DeleteFavoriteSong(string UserId, string SongId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.DeleteFavoriteSong(UserId, SongId);
        }
        public static List<SongMusic> GetFavoriteSongByUserId(string UserId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetFavoriteSongByUserId(UserId);
        }
        public static int GetTheNumberOfAppearanceInUserByGivenSong(string SongId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTheNumberOfAppearanceInUserByGivenSong(SongId);
        }
        public static List<SongMusic> GetTop5SongsForUser(string UserId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTop5SongsForUser(UserId);
        }
        public static List<SongMusic> GetTop5SongsForArtist(string ArtistName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTop5SongsForArtist(ArtistName);
        }
        public static List<SongMusic> GetTop10GlobalSongs()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTop10GlobalSongs();
        }
        public static int GetTheNumberPlayedForGivenSong(string SongId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetTheNumberPlayedForGivenSong(SongId);
        }
    }
}
