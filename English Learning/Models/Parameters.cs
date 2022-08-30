using System;

namespace English_Learning.Models
{
    public class Parameter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }


        //public StudyMethods DefaultStudyMothod { get; set; }//метод изуения по умолчанию, 
        //public bool NotificationIsPush { get; set; }
        //public DateTime AppActivityStartDate { get; set; }
        //public DateTime AppActivityEndDate { get; set; }
        //public int AppIndexLanguage { get; set; }
    }
}
