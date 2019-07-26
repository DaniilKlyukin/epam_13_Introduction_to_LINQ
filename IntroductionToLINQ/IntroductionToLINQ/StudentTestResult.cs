using System;

namespace IntroductionToLINQ
{
    class StudentTestResult
    {
        public string StudentName { get; set; }
        public string TestName { get; set; }
        public DateTime TestDate { get; set; }
        public int Mark { get; set; }

        public StudentTestResult(string studentName, string testName, DateTime date, int mark)
        {
            StudentName = studentName;
            TestName = testName;
            TestDate = date;
            Mark = mark;
        }
    }
}
