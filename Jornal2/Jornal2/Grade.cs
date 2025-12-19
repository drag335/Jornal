using System;

namespace Jornal2
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }
    }
}
