using System;

namespace English_Learning.Models
{
    public class Word
    {
        public string Id { get; set; }
        public string ForeignWord { get; set; }
        public string Translation {  get; set; }
        public StudyMethods StudyMethod { get; set; }
        public DateTime DateOfInsertion { get; set; }
        public int Level { get; set; }
        public DateTime LastViewed { get; set; }
        public DateTime NextViewing{ get; set; }
        public bool IsArchived { get; set; }


        public void LevelUp()
        {


            switch (StudyMethod)
            {
                case StudyMethods.Pimsler:

                    break;
                case StudyMethods.Leitner:

                    break;

            }
        }

    }
}
