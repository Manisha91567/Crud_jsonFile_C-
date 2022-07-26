using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();

            Console.WriteLine("Choose option : 1: Get, 2: Insert, 3: Update, 4: Delete \n");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    obj.getInfo();
                    break;
                case "2":
                    obj.addInfo();
                    break;
                case "3":
                    obj.updateInfo();
                    break;
                case "4":
                    obj.deleteInfo();
                    break;
                default:
                    Main(null);
                    break;
            }
            Console.ReadLine();

        }

        private string jsonFile = @"C:\Users\gaikw\source\repos\ConsoleApp2\ConsoleApp2\student.json";

        private void getInfo()
        {

            var json = File.ReadAllText(jsonFile);

            var jObject = JObject.Parse(json);

            if (jObject != null)
            {

                JArray Arr = (JArray)jObject["users"];
                if (Arr != null)
                {
                    foreach (var item in Arr)
                    {
                        Console.WriteLine("userId: " + item["userId"]);
                        Console.WriteLine("firstName: " + item["firstName"]);
                        Console.WriteLine("lastName: " + item["lastName"]);
                        Console.WriteLine("phoneNumber: " + item["phoneNumber"]);
                        Console.WriteLine("emailAddress: " + item["emailAddress"]);
                        Console.WriteLine();
                    }
                }
            }
        }

        private void addInfo()
        {
            Console.WriteLine("Enter user id: ");
            var id = Console.ReadLine();
            Console.WriteLine("\nEnter fisrt name: ");
            var fname = Console.ReadLine();
            Console.WriteLine("\nEnter last name: ");
            var lname = Console.ReadLine();
            Console.WriteLine("\nEnter phone number: ");
            var number = Console.ReadLine();
            Console.WriteLine("\nEnter email: ");
            var email = Console.ReadLine();

            JObject newvalue = new JObject(
                new JProperty("userId", id),
                new JProperty("firstName", fname),
                new JProperty("lastName", lname),
                new JProperty("phoneNumber",number),
                new JProperty("emailAddress", email));

            var json = File.ReadAllText(jsonFile);
            var jsonObj = JObject.Parse(json);
            var usersArrary = jsonObj.GetValue("users") as JArray;
            usersArrary.Add(newvalue);

            jsonObj["users"] = usersArrary;
            string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonFile, newJsonResult);
            Console.WriteLine("user is added");
        }

        private void updateInfo()
        {
            string json = File.ReadAllText(jsonFile);

            var jObject = JObject.Parse(json);
            JArray usersArrary = (JArray)jObject["users"];
            Console.Write("Enter user Id for update: ");
            var userId = Convert.ToInt32(Console.ReadLine());

            if (userId > 0)
            {
                Console.WriteLine("\nEnter fisrt name: ");
                var fname = Console.ReadLine();
                Console.WriteLine("\nEnter last name: ");
                var lname = Console.ReadLine();
                Console.WriteLine("\nEnter phone number: ");
                var number = Console.ReadLine();
                Console.WriteLine("\nEnter email: ");
                var email = Console.ReadLine();

                foreach (var user in usersArrary.Where(obj => obj["userId"].Value<int>() == userId))
                {
                    user["firstName"] = !string.IsNullOrEmpty(fname) ? fname : "";
                    user["lastName"] = !string.IsNullOrEmpty(lname) ? lname : "";
                    user["phoneNumber"] = !string.IsNullOrEmpty(number) ? number : "";
                    user["emailAddress"] = !string.IsNullOrEmpty(email) ? email : "";
                }
                jObject["users"] = usersArrary;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFile, output);
                Console.WriteLine("user information is updated");
            }
            else
            {
                Console.Write("Invalid user Id, Try Again!");
                updateInfo();
            }
        }
        private void deleteInfo()
        {

            var json = File.ReadAllText(jsonFile);

            var jObject = JObject.Parse(json);
            JArray usersArrary = (JArray)jObject["users"];
            Console.Write("Enter user Id to delete user : ");
            var userId = Convert.ToInt32(Console.ReadLine());

            if (userId > 0)
            {

                var userToDeleted = usersArrary.FirstOrDefault(obj => obj["userId"].Value<int>() == userId);
                usersArrary.Remove(userToDeleted);
                Console.Write("user is deleted");

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFile, output);
            }
            else
            {
                Console.Write("Invalid user Id, Try Again!");
                updateInfo();
            }
        }
    }
}
