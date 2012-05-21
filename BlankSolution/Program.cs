using System;
using System.IO;

namespace BlankSolution
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length != 1)
         {
            PrintUsage();
            return;
         }

         string slnName = args[0];

         slnName = slnName.EndsWith(".sln", StringComparison.InvariantCultureIgnoreCase)
            ? slnName
            : slnName + ".sln";

         FileInfo sln = new FileInfo(slnName);

         if (sln.Exists)
         {
            for (; ; )
            {
               Console.WriteLine("There's a file named " + sln.Name + ", overwrite? (y/n)");
               var key = Console.ReadKey();
               Console.WriteLine();

               if (key.KeyChar == 'n')
                  return;
               if (key.KeyChar == 'y')
                  break;
            }
         }

         try
         {
            File.WriteAllBytes(slnName, new byte [] { 0xEF, 0xBB, 0xBF });
            File.AppendAllText(slnName, SolutionPattern);
         }
         catch (Exception)
         {
            Console.WriteLine("Could not write to file named " + sln.Name);
         }
      }

      private static void PrintUsage()
      {
         Console.WriteLine("Usage: blanksolution.exe <solution name>");
      }

      private static string SolutionPattern = "\r\n" +
@"Microsoft Visual Studio Solution File, Format Version 11.00
# Visual Studio 2010
Global
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
";
   }
}
