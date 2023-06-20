using System.Text.Json;
using System.Xml.Serialization;

namespace Lesson08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var department = new Department()
            {
                DepartmentName = "Receptions",
                Employees = new List<Employee>
                {
                    new Employee
                    {
                        EmployeeName = "Zikirillo"
                    },
                    new Employee
                    {
                        EmployeeName = "Ronaldo"
                    },
                    new Employee
                    {
                        EmployeeName = "Messi"
                    },

                    new Employee
                    {
                        EmployeeName = "Anvar"
                    }
                }
            };

            WriteToTextJson(department);

            XML_DE_Serializations(department);
        }

        static void WriteToTextJson(Department department)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\JsonFiles.json";

            if (!File.Exists(path))
            {
                File.Create(path).Close();

                using (StreamWriter sw = new StreamWriter(path))
                {
                    string json = JsonSerializer.Serialize(department);

                    sw.WriteLine(json);

                    Console.WriteLine("Informations added successfully!");
                }
            }

            else
            {
                ReadToTextJson(path, department);
            }
        }

        static void ReadToTextJson(string path, Department department)
        {

            using (StreamReader sr = new StreamReader(path))
            {
                string alltext = sr.ReadToEnd();

                var json = JsonSerializer.Deserialize<Department>(alltext);

                department.DepartmentName = json.DepartmentName;
                department.Employees = json.Employees;

                Console.WriteLine($"The informations in ({path}) is reloaded into the department object.");
            }
        }

        static void XML_DE_Serializations(Department department)
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(Department));

            string pathxml = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\XMLFiles.xml";


            using (FileStream fs = new(pathxml, FileMode.OpenOrCreate))
            {
                xmlserializer.Serialize(fs, department);

                Console.WriteLine("Object has been serialized");
            }

            using (FileStream fs = new(pathxml, FileMode.OpenOrCreate))
            {

                var xml = xmlserializer.Deserialize(fs) as Department;

                department.DepartmentName = xml.DepartmentName;
                department.Employees = xml.Employees;

                Console.WriteLine("succes xml");
            }
        }
    }

    public class Employee
    {
        public string EmployeeName { get; set; }

    }

    public class Department
    {
        public string DepartmentName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}