using System;

namespace English_Learning.Models
{
    public class Word
    {
        public string Id { get; set; }
        public string ForeignWord { get; set; }
        public string Translation {  get; set; }
        public bool StudyMothod { get; set; }
        public DateTime DateOfInsertion { get; set; }
        public int MethodLevel { get; set; }
        public DateTime LastViewed { get; set; }
        public DateTime NextViewing{ get; set; }
        public bool IsArchived { get; set; }
    }
}
