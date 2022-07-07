using System;
using System.Collections.Generic;

namespace Schedule2._0
{
    class Student
    {
        public string name { get; set; }
        public int studentID { get; set; }
        static int ID = 1;
        public Student(string name)
        {
            this.name = name;
            studentID = ID++;
        }
        public void PrintStudentInfo()
        {
            Console.WriteLine($"ID: {studentID}, name: {name}");
        }

        public void ChangeStudentInfo()
        {
            Console.Write("Enter new name:\t");
            name = Console.ReadLine();
        }
    }

    class Group
    {
        public List<Student> groupOfStudent = new List<Student>();
        public string nameOfGroup { get; set; }
        public string teacherOfGroup { get; set; }
        public string firstDay { get; set; }
        public string secondDay { get; set; }

        public Group(string nameOfGroup, string teacherOfGroup, string firstDay, string secondDay)
        {
            this.nameOfGroup = nameOfGroup;
            this.teacherOfGroup = teacherOfGroup;
            this.firstDay = firstDay;
            this.secondDay = secondDay;
        }

        public void ChangeGroupInfo()
        {
            Console.WriteLine("1. change group name");
            Console.WriteLine("2. change teacher of group");
            Console.WriteLine("3. change date of studying");
            Console.WriteLine("4. delete student");
            Console.WriteLine("5. add student");
            Console.WriteLine("0. exit from editing");

            int.TryParse(Console.ReadLine(), out int answer);

            switch (answer)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Enter new group name:\t");
                    nameOfGroup = Console.ReadLine();
                    break;
                case 2:
                    Console.Clear();
                    Console.Write("Enter new teacher of group:\t");
                    teacherOfGroup = Console.ReadLine();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Enter new date of styding");
                    Console.Write("First day = ");
                    firstDay = Console.ReadLine();
                    Console.Write("Second day = ");
                    secondDay = Console.ReadLine();
                    break;
                case 4:
                    DeleteStudent();
                    break;
                case 5:
                    AddStudent();
                    break;
                case 0:
                    break;
            }

        }

        public void AddStudent()
        {
            Console.WriteLine("Enter name:\t");
            groupOfStudent.Add(new Student(Console.ReadLine()));
        }

        private Student FindStudentByID(int id)
        {
            foreach (var i in groupOfStudent)
                if (i.studentID == id)
                    return i;
            return null;
        }

        public void DeleteStudent()
        {
            Console.Write("Enter ID:\t");
            int.TryParse(Console.ReadLine(), out int id);
            Student removedStudent = FindStudentByID(id);
            if(removedStudent == null)
            {
                Console.Write("Student was not found");
                Console.WriteLine("press ENTER to continue...");
                Console.ReadLine();
                return;
            }
            groupOfStudent.Remove(removedStudent);
        }

        
        public void PrintStudentsOfGroup()
        {
            foreach(var student in groupOfStudent)
            {
                student.PrintStudentInfo();
            }
        }
        public void PrintGroupInfo()
        {
            Console.WriteLine($"Group:\t{nameOfGroup}");
            Console.WriteLine($"Teacher:\t{teacherOfGroup}");
            Console.WriteLine($"First day:\t{firstDay}");
            Console.WriteLine($"Second day:\t{secondDay}");
            Console.WriteLine();
            PrintStudentsOfGroup();
        }

    }
    class Program
    {
        static Group FindGroupInList(List<Group> groups, string searchName)
        {
            foreach (var i in groups)
                if (i.nameOfGroup == searchName)
                    return i;
            return null;
        }

        static void HaventGroupMessage()
        {
            Console.WriteLine("You haven't any group in list.");
            Console.WriteLine("press ENTER to continue...");
            Console.ReadLine();
        }
        
        static void NotFoundGroupMessage()
        {
            Console.WriteLine("Group was not found");
            Console.WriteLine("press ENTER to continue...");
            Console.ReadLine();
        }

        static bool CheckAddNewStudent()
        {
            string answer = default;
            while (answer != "yes" && answer != "no")
            {
                Console.Clear();
                Console.Write("Do you want to add new student? (yes/no):\t");
                answer = Console.ReadLine();
                if (answer != "yes" && answer != "no")
                {
                    Console.Clear();
                    Console.WriteLine("Excuse me, i don't understand");
                    Console.WriteLine("press ENTER to continue...");
                    Console.ReadLine();

                }
            }
            return answer == "yes";
        }

        static void AddStudent(Group group)
        {
            Console.WriteLine("Enter student name:\t");
            string name = Console.ReadLine();
            group.groupOfStudent.Add(new Student(name));
        }

        static void Main(string[] args)
        {
            List<Group> groups = new List<Group>();
            bool statusOfProgram = true;
            int action = default;
            string groupName, teacherName, firstDay, secondDay;
            while (statusOfProgram)
            {
                Console.Clear();
                Console.WriteLine("1. Check the schedule of group");
                Console.WriteLine("2. Create group");
                Console.WriteLine("3. Change group information");
                Console.WriteLine("0. exit from program");
                if (!int.TryParse(Console.ReadLine(), out action))
                    action = -1;
                switch (action)
                {
                    case 1:
                        if (groups.Count == 0)
                            HaventGroupMessage();
                        else
                        {
                            Console.Write("Enter group name:\t");
                            Group searchGroup = FindGroupInList(groups, Console.ReadLine());
                            if (searchGroup == null)
                                NotFoundGroupMessage();
                            else
                            {
                                Console.Clear();
                                searchGroup.PrintGroupInfo();
                                Console.WriteLine("press ENTER to continue...");
                                Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.Write("Enter group name:\t");
                        groupName = Console.ReadLine();
                        Console.Write("Enter teacher name:\t");
                        teacherName = Console.ReadLine();
                        Console.Write("Enter the first study day of the week:\t");
                        firstDay = Console.ReadLine();
                        Console.Write("Enter the second study day of the week:\t");
                        secondDay = Console.ReadLine();
                        groups.Add(new Group(groupName, teacherName, firstDay, secondDay));

                        Console.Clear();
                        bool statusOfAddingStudent = CheckAddNewStudent();
                        while (statusOfAddingStudent)
                        {
                            AddStudent(groups[groups.Count - 1]);
                            Console.Clear();
                            statusOfAddingStudent = CheckAddNewStudent();
                        }
                        break;
                    case 3:
                        if (groups.Count == 0)
                            HaventGroupMessage();
                        else
                        {
                            Console.Write("Enter group name:\t");
                            Group searchGroup = FindGroupInList(groups, Console.ReadLine());
                            if (searchGroup == null)
                                NotFoundGroupMessage();
                            else
                            {
                                Console.Clear();
                                searchGroup.ChangeGroupInfo();
                                Console.WriteLine("press ENTER to continue...");
                                Console.ReadLine();
                            }
                        }
                        break;

                    case 0:
                        statusOfProgram = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("invalid action");
                        Console.Write("\npress ENTER to continue...");
                        Console.ReadLine();
                        break;
                }

            }

        }
    }
}
