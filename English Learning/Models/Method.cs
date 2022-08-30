using System;
using System.Collections.Generic;
using System.Text;

namespace English_Learning.Models
{
    public enum StudyMethods
    {
        Pimsler,
        Leitner
    }
    public class Method
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public StudyMethods StudyMethod { get; set; }
        public TimeSpan[] Intervals { get; set; }
        public bool FromLastContact { get; set; }
        public int CountLevels { get; set; }

        public static DateTime GetNextViewingDateTime(StudyMethods studyMethod, DateTime dateOfInsertion, DateTime lastViewed, int level)
        {
            DateTime startDateTime;
            TimeSpan[] periods;
            TimeSpan period;

            switch (studyMethod)
            {
                case StudyMethods.Pimsler:
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
                    startDateTime = dateOfInsertion;
                    break;
                case StudyMethods.Leitner:
                    periods = new TimeSpan[]
                    {
                        new TimeSpan(1,0,0),
                        new TimeSpan(2,0,0),
                        new TimeSpan(7,0,0,0),
                        new TimeSpan(14,0,0,0),
                        new TimeSpan(30,0,0,0)
                    };
                    startDateTime = lastViewed;
                    break;

                default:
                    periods = new TimeSpan[0];
                    startDateTime = DateTime.MinValue;
                    break;
            }

            if (level > periods.Length)
                period = TimeSpan.Zero;
            else
                period = periods[level];

            return startDateTime - period;
        }

    }
}
