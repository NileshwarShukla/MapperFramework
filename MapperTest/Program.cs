using MapperLibrary;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Source proj = new Source();
            proj.Prop1 = "Test";
            proj.DateAdded = DateTime.Now;
            proj.Area = "Country";
            proj.User = "Nilesh";
            proj.Address = new Nested();
            proj.Address.Address1 = "Val1";
            proj.Address.Address2 = "Val2";
            proj.Address.Address3 = "Val3";

            

            string jsonString = File.ReadAllText(Path.GetFullPath("json2.json"));
            Target target = new Target();

            ExtendedActions actions = new ExtendedActions();
            Mapper<Source> mapp = new Mapper<Source>(actions);
            target = (Target)mapp.CreateMapping(proj, target, jsonString);
            Console.WriteLine(JsonConvert.SerializeObject(target));
            Console.ReadLine();
        }
    }
}
