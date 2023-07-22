using Server.Moodle.DAL;

namespace Server.Moodle
{
    public class QuestionMusic
    {
        public QuestionMusic(string question, string answer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
        {
            Question = question;
            Answer = answer;
            WrongAnswer1 = wrongAnswer1;
            WrongAnswer2 = wrongAnswer2;
            WrongAnswer3 = wrongAnswer3;
        }

        public string Question { get; set; }
        public string Answer { get; set; }
        public string WrongAnswer1 { get; set; }
        public string WrongAnswer2 { get; set; }
        public string WrongAnswer3 { get; set; }
        public static QuestionMusic CreateQustion1WhoCreatedTheSong()
        {
            DBservices dBservices = new DBservices();
            return dBservices.CreateQustion1WhoCreatedTheSong();
        }
        public static QuestionMusic CreateQustion2WhatSongBelongToArtist()
        {
            DBservices dBservices = new DBservices();
            return dBservices.CreateQustion2WhatSongBelongToArtist();
        }
        public static QuestionMusic CreateQustion3WhatPicBelongToArtist()
        {
            DBservices dBservices = new DBservices();
            return dBservices.CreateQustion3WhatPicBelongToArtist();
        }
        public static QuestionMusic CreateQustion4WhatPicBelongToSong()
        {
            DBservices dBservices = new DBservices();
            return dBservices.CreateQustion4WhatPicBelongToSong();
        }
        public static QuestionMusic CreateQustion5WhatIsTheDurationForSong()
        {
            DBservices dBservices = new DBservices();
            return dBservices.CreateQustion5WhatIsTheDurationForSong();
        }
        public static QuestionMusic CreateQustion6whichSongBeginWith()
        {
            DBservices dBservices = new DBservices();
            return dBservices.CreateQustion6whichSongBeginWith();
        }
    }
}
