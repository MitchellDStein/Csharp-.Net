using System;
using System.Reflection;
using static System.Console;
using System.Linq;                      // to use OrderByDescending
using System.Runtime.CompilerServices;  // to use CompilerGeneratedAttribute

namespace Working_With_Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            WriteLine("Assembly metadata:");
            WriteLine($" Full name: {assembly.FullName}");
            WriteLine($" Location: {assembly.Location}");

            var attributes = assembly.GetCustomAttributes();
            WriteLine($"\n Attributes:");
            foreach (Attribute attr in attributes)
            {
                WriteLine($" {attr.GetType()}");
            }

            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
            WriteLine($" Version: {version.InformationalVersion}");
            WriteLine($" Company: {company.Company}");


            //Creating custom attributes
            WriteLine("\n== Creating custom attributes ==");
            WriteLine($"* Types:");
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                // optional challenge to not show CompilerGeneratedAttributes
                var compilerGenerated = type.GetCustomAttribute<CompilerGeneratedAttribute>();
                if (compilerGenerated != null) break;
                
                WriteLine();
                WriteLine($"Type: {type.FullName}");
                MemberInfo[] members = type.GetMembers();
                foreach (MemberInfo member in members)
                {
                    WriteLine($"{member.MemberType}: {member.Name} ({member.DeclaringType.Name})");
                    var coders = member.GetCustomAttributes<CoderAttribute>().OrderByDescending(c => c.LastModified);
                    foreach (CoderAttribute coder in coders)
                    {
                        WriteLine("-> Modified by {0} on {1}", coder.Coder, coder.LastModified.ToShortDateString());
                    }
                }
            }
        }

        // coder attributes are created in CoderAttribute.cs
        [Coder("Mitchell Stein", "01 June 2020")]
        [Coder("John Smith", "15 October 1204")]
        public static void DoStuff()
        {
        }
    }
}
