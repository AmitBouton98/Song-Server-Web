namespace Server.Moodle
{
    public interface IComment<T>
    {
        public bool insertOrUpdateComment(T c);
        public bool deleteComment();
        public bool getAllComments();
    }
}
