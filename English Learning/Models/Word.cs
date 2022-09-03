using System;

namespace English_Learning.Models
{
    public class Word
    {
        public string Id { get; set; }
        public string ForeignWord { get; set; }
        public string Translation {  get; set; }
        public StudyMethodsEnum StudyMethodEnum { get; set; }
        public Method StudyMethod
        {
            get
            {
                return StudyMethods.GetStudyMethodFromEnum(StudyMethodEnum);
            }
        }
        public DateTime DateOfInsertion { get; set; }
        public int Level { get; set; }
        public DateTime LastViewed { get; set; }
        public DateTime NextViewing
        {
            get { return GetNextViewingDateTime(); }
        }
        public bool IsArchived { get; set; }

        public void LevelUp()
        {
            switch (StudyMethodEnum)
            {
                case StudyMethodsEnum.Pimsler:

                    break;
                case StudyMethodsEnum.Leitner:

                    break;
            }
        }

        private DateTime GetNextViewingDateTime()
        {
            DateTime startDateTime;
            TimeSpan[] periods;
            TimeSpan period;

            switch (StudyMethodEnum)
            {
                case StudyMethodsEnum.Pimsler:
                    periods = new TimeSpan[]
                    {
                        new TimeSpan(0,30,0),
                        new TimeSpan(8,0,0),
                        new TimeSpan(1,0,0,0),
                        new TimeSpan(3,0,0,0),
                        new TimeSpan(10,0,0,0),
                        new TimeSpan(30,0,0,0),
                        new TimeSpan(90,0,0,0)
                    };
                    startDateTime = DateOfInsertion;
                    break;
                case StudyMethodsEnum.Leitner:
                    periods = new TimeSpan[]
                    {
                        new TimeSpan(1,0,0),
                        new TimeSpan(2,0,0),
                        new TimeSpan(7,0,0,0),
                        new TimeSpan(14,0,0,0),
                        new TimeSpan(30,0,0,0)
                    };
                    startDateTime = LastViewed;
                    break;

                default:
                    periods = new TimeSpan[0];
                    startDateTime = DateTime.MinValue;
                    break;
            }

            if (Level > periods.Length)
                period = TimeSpan.Zero;
            else
                period = periods[Level];

            return startDateTime + period;
        }

    }
}
