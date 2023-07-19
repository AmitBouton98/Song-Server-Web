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
        public string UrlLink { get; private set; }
        public string YoutubeId { get; private set; }
        public string Duration { get; private set; }
        public SongMusic(string id, string artistName, string name, string likes, string lyricLink, string urlLink, string youtubeId, string duration)
        {
            Id = id;
            ArtistName = artistName;
            Name = name;
            Likes = likes;
            LyricLink = lyricLink;
            UrlLink = urlLink;
            YoutubeId = youtubeId;
            Duration = duration;    
        }
        public static bool AddFavoriteSong(string UserId, string SongId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.AddFavoriteSong(UserId, SongId);
        }
        public static int GetNumberOfSongs()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetNumberOfSongs();
        }
        public static bool ChangeYoutubeIdSong(string SongId, string YoutubeId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.ChangeYoutubeIdSong(SongId, YoutubeId);
        }
        public static bool ChangeSongUrl(string SongId, string Url)
        {
            DBservices dBservices = new DBservices();
            return dBservices.ChangeSongUrl(SongId, Url);
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
        public static List<SongMusic> GetSongsUserMightLike(string UserId)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetSongsUserMightLike(UserId);
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
        public static bool InsertOrUpdateSong(SongMusic song)
        {
            DBservices dBservices = new DBservices();
            return dBservices.InsertSongOrUpdate(song);
        }
        public static bool Delete(string id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.DeleteSong(id);
        }
        public static SongMusic GetSongById(string id)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetSongById(id);
        }
        public static SongMusic GetSongByName(string name)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetSongByName(name);
        }
        public static List<SongMusic> GetSongByText(string text)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetSongByText(text);
        }
        public static List<SongMusic> GetAllSongs()
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAllSongs();
        }
        public static List<SongMusic> GetSongByArtistName(string artistName)
        {
            DBservices dBservices = new DBservices();
            return dBservices.GetAllArtistSongs(artistName);
        }
    }
}
