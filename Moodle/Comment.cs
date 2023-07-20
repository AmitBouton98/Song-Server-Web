namespace Server.Moodle
{
    public  abstract class Comment
    {
        public Comment(string userId, string id, string text, DateTime createDate, string stars)
        {
            UserId = userId;
            Id = id;
            Text = text;
            CreateDate = createDate;
            Stars = stars;
        }

        public string UserId { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }  
        public string Stars { get; set; }
    }
}
