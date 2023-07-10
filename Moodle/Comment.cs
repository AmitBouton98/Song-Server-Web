namespace Server.Moodle
{
    public  abstract class Comment
    {
        protected Comment(string userId, string id, string text, string createDate)
        {
            UserId = userId;
            Id = id;
            Text = text;
            CreateDate = createDate;
        }

        public string UserId { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string CreateDate { get; set; }  
        
    }
}
