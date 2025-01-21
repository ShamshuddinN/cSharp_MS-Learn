using EducationLib;
namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {

        List<Student> students = new List<Student> {
            new Student() {FirstName = "Sam", LastName = "Morgan", ID = 1001, Year = GradeLevel.FirstYear, 
            Scores = new List<int> {20, 40, 95}, DepartmentID = 2 },

            new Student() {FirstName = "David", LastName = "Bravestone", ID = 1002, Year = GradeLevel.SecondYear, 
            Scores = new List<int> {78, 34, 56}, DepartmentID = 1 },
            
            new Student() {FirstName = "John", LastName = "Doe", ID = 1003, Year = GradeLevel.FirstYear, 
            Scores = new List<int> {76, 38, 54}, DepartmentID = 5 },
            
            new Student() {FirstName = "Alex", LastName = "Johnson", ID = 1004, Year = GradeLevel.FourthYear, 
            Scores = new List<int> {45, 54, 64}, DepartmentID = 3 },

            new Student() {FirstName = "Robert", LastName = "Spancer", ID = 1005, Year = GradeLevel.ThirdYear, 
            Scores = new List<int> {36, 82, 53}, DepartmentID = 4 },
            
            new Student() {FirstName = "Ahmed", LastName = "Khan", ID = 1006, Year = GradeLevel.ThirdYear, 
            Scores = new List<int> {85, 64, 61}, DepartmentID = 2 },
            
            new Student() {FirstName = "Hamza", LastName = "Nawaz", ID = 1007, Year = GradeLevel.FirstYear, 
            Scores = new List<int> {35, 45, 63}, DepartmentID = 3 },
            
            new Student() {FirstName = "Brian", LastName = "Yu", ID = 1008, Year = GradeLevel.FourthYear, 
            Scores = new List<int> {96, 47, 64}, DepartmentID = 1 },

            new Student() {FirstName = "David", LastName = "Malan", ID = 1009, Year = GradeLevel.ThirdYear, 
            Scores = new List<int> {63, 47, 67}, DepartmentID = 5 },

        };


        List<Department> departments = new List<Department>() {
                new Department() {
                    Name = "Botany",
                    ID = 1,
                    TeacherID = 100
                },
                new Department () { Name = "Biology", ID = 2, TeacherID = 101 },
                new Department () { Name = "Zoology", ID = 3, TeacherID = 102 },
                new Department () { Name = "Physics", ID = 4, TeacherID = 103 },
                new Department () { Name = "Chemistry", ID = 5, TeacherID = 104 },
            };


            List<Teacher> teachers = new List<Teacher>() {
                new Teacher() {First = "George", Last = "Amelton", City = "California", ID = 101},
                new Teacher() {First = "Betheny", Last = "J", City = "New York", ID = 102},
                new Teacher() {First = "Martha", Last = "Samson", City = "Dallas", ID = 103},
                new Teacher() {First = "Alex", Last = "Johnson", City = "California", ID = 104}
            };



            System.Console.WriteLine("Query 1: Display Student Firstname, Lastname and Department Name");
            System.Console.WriteLine();

            // **using normal query syntax**:

            // var query1 = from stud in students join dep in departments 
            // on stud.DepartmentID equals dep.ID
            // select new {stud.FirstName, stud.LastName, dep.Name};

            // foreach (var studEle in query1)
            // {
            //     System.Console.WriteLine($"Fn: {studEle.FirstName}, LN: {studEle.LastName}, Dep. Name: {studEle.Name}");
            // }

            // ** Using Method Syntax** :
            var query1 = students.Join(
                departments,
                student => student.DepartmentID,
                department => department.ID,
                (student, department) => new { FullName =  $"{student.FirstName} {student.LastName}", deptName = department.Name }
            ); 

            foreach (var studEle in query1)
            {
                System.Console.WriteLine($"Full name: {studEle.FullName}, Dep. Name: {studEle.deptName}");
            }

            System.Console.WriteLine("-----------------------------------------------");
            System.Console.WriteLine();

            System.Console.WriteLine("Q2: Display Department name and teacher name: ");
            var query2 = departments.Join(
                teachers,
                dept => dept.TeacherID,
                techer => techer.ID,
                (dept, techer) => new {dep_Name = dept.Name, techer_Name = $"{techer.First} {techer.Last}"}
            );

            foreach (var tec in query2)
            {
                System.Console.WriteLine($"Teacher: {tec.techer_Name}, Department: {tec.dep_Name}");
            }

            
            System.Console.WriteLine("-----------------------------------------------");
            System.Console.WriteLine();

            System.Console.WriteLine("Q3: Identify and display teacher/s who is/are also student/s ");
            var query3 = students.Join(
                teachers,
                stud => new {fn = stud.FirstName, ln = stud.LastName},
                teacher => new {fn = teacher.First, ln = teacher.Last},
                // observe that 'fn' and 'ln' variables should be same in both new objects above
                (stud, teacher ) => new  {CandidateName = $"{teacher.First} {teacher.Last}"}
            );

            foreach (var candidate in query3)
            {
                System.Console.WriteLine($"Name of Teacher Cum Student Candidate/s: {candidate.CandidateName}");
            }

            System.Console.WriteLine("-----------------------------------------------");
            System.Console.WriteLine();

            System.Console.WriteLine("Q4: Display the student name and teacher name who runs" 
            + "\n the department in which student is studying: ");

            var query4 = students.Join( 
                departments, 
                stud => stud.DepartmentID,
                dept => dept.ID,
            
            (stud, dept) => new { StudentName = $"{stud.FirstName} {stud.LastName}", teacherId = dept.TeacherID } )
            
            .Join(teachers, 
            tId => tId.teacherId, 
            teacher => teacher.ID, 
            (snd, teacherTbl) => new {
                teacherName = $"{teacherTbl.First} {teacherTbl.Last}",
                studentName = snd.StudentName 
                } );

            foreach (var stud in query4)
            {
                System.Console.WriteLine($"The Student {stud.studentName} studies in department run by {stud.teacherName}");
            }

            System.Console.WriteLine("-----------------------------------------------");
            System.Console.WriteLine();
            
            System.Console.WriteLine("Q5. Display Student name and their respective department name");
            var query5 = students.Join(
                departments,
                stud => stud.DepartmentID,
                dept => dept.ID,
                (sObj, dObj) => new {studentName = $"{sObj.FirstName} {sObj.LastName}", departmentName = dObj.Name}
            );

            foreach (var sdObj in query5)
            {
                System.Console.WriteLine($"Student {sdObj.studentName} studies in {sdObj.departmentName}");
            }


            System.Console.WriteLine("-----------------------------------------------");
            System.Console.WriteLine();
            
            System.Console.WriteLine("Q6. Display Each Department and Students Studying in that department: ");
            
            var query6 = departments.GroupJoin(
                students,
                dept => dept.ID,
                stud => stud.DepartmentID,
                (dept, studs) => new {departmentName = dept.Name, studs}
            );

            foreach (var dsObj in query6)
            {
                System.Console.WriteLine($"In the department of {dsObj.departmentName} following students are studying: ");
                System.Console.WriteLine();
                foreach (Student studObj in dsObj.studs)
                {
                    System.Console.WriteLine($"- {studObj.FirstName} {studObj.LastName}");
                }
                System.Console.WriteLine();
                System.Console.WriteLine("-------------");
            }



    }
}
