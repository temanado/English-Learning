using System;

namespace English_Learning.Models
{
    public class Configs
    {
        bool DefaultStudyMothod { get; set; }//метод по умолчанию, 
        bool NotificationMethod { get; set; }
        DateTime AppActivityStartDate { get; set; }
        DateTime AppActivityEndDate { get; set; }
        int AppIndexLanguage { get; set; }
    }
}
