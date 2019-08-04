//***************************************************************************
//File: Program.cs
//
//Purpose: To allow the user to read, write, display data about currency, 
//  language, and country objects. The user will be able read from
//  to .json and .xml files. The user will be able to write in .json or
//  .xml code to a file. A dynamic library that contains these objects
//  was added to this solutions references. This program has the ability to
//  store large data sets of data from files to objects. These objects then
//  have the ability to display the information on screen or convert the
//  file type.
//
//Written By: Timothy Negron
//
//Compiler: Visual Studio C# 2017
//
//Update Information
//------------------
//
//***************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization; // Needed to write to XML files
using System.Runtime.Serialization.Json; // Needed to write to Json files
using System.Text;
using System.Threading.Tasks;
using CSprogramming_CS_DLL_for_Country_Data; // Dynamic Link Library Project 3

namespace CSprogramming_CS_Project3_Menu_Using_Country_DLL
{
    class Program
    {
        #region Main Method
        //***************************************************************************
        //Method: Main
        //
        //Purpose: To display a menu to the user that will give them 8 options that
        //  the program has the ability to do regarding reading, writing, and
        //  display data about objects from a dynamic library that was added as a
        //  reference. Objects in use are Country, Currency, Language.
        //***************************************************************************
        static void Main(string[] args)
        {
            List<Country> ListOfCountryData = new List<Country>();

            string FILENAME;

            string userChoice; // For switch statment/user's desicion options

            // Display menu and loop until user decides to end program
            do
            {
                // Display the menu to the user, If 16 entered, loop will end
                Console.WriteLine("\nCountry Menu");
                Console.WriteLine("-------------");
                Console.WriteLine("1 - Read List of Country from JSON file");
                Console.WriteLine("2 - Read List of Country from XML file");
                Console.WriteLine("3 - Write List of Country JSON file");
                Console.WriteLine("4 - Write List of Country XML file");
                Console.WriteLine("5 - Display All Country List Items on Screen");
                Console.WriteLine("6 - Find and display country by name");
                Console.WriteLine("7 - Find and display countries that use a given currency code");
                Console.WriteLine("8 - Exit");
                Console.Write("\nEnter Choice: ");

                // Read the user's choice and store it in userChoice string variable.
                userChoice = Console.ReadLine();

                // Beginning of switch statement
                switch (Convert.ToInt32(userChoice))
                {
                    case 1: // Read List of Country from JSON file
                        {
                            // Get file name from user
                            Console.Write("\nEnter filename: ");
                            FILENAME = Console.ReadLine();

                            // Construct a filestream obj with the filename, open the file, and give obj read access
                            FileStream readFILE = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);

                            // Construct a streamreader obj with the filestream obj and encode with UTF-8
                            StreamReader streamReader = new StreamReader(readFILE, Encoding.UTF8);

                            // Initialize a string variable with data in streamReader
                            string jsonString = streamReader.ReadToEnd();

                            // Initialize byte variable with encoding abstract data type method, use string with json data as parameter
                            byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);

                            // Construct a memorystream obj with byteArray variable
                            MemoryStream stream = new MemoryStream(byteArray);

                            // Construct a DCJS Object with List<Country> obj
                            DataContractJsonSerializer serListOfCountryData = new DataContractJsonSerializer(typeof(List<Country>));

                            // Initialize ListOfCoutries with DataConstractJsonSerialize read object method
                            ListOfCountryData = (List<Country>)serListOfCountryData.ReadObject(stream);

                            readFILE.Close();
                        }
                        break;
                    case 2: // Read List of Country from XML file
                        {
                            // Get file name from user
                            Console.Write("\nEnter filename: ");
                            FILENAME = Console.ReadLine();

                            FileStream readFILE = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
                            StreamReader streamReader = new StreamReader(readFILE, Encoding.UTF8);
                            string jsonString = streamReader.ReadToEnd();
                            byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);
                            MemoryStream stream = new MemoryStream(byteArray);
                            DataContractSerializer serListOfCountryData = new DataContractSerializer(typeof(List<Country>));
                            ListOfCountryData = (List<Country>)serListOfCountryData.ReadObject(stream);
                            readFILE.Close();
                        }
                        break;
                    case 3: // Write List of Country to JSON file
                        {
                            // User decides name of file to write too
                            Console.Write("\nEnter filename: ");
                            FILENAME = Console.ReadLine();

                            // Construct DCJS object with Country list
                            DataContractJsonSerializer serListOfCountryData = new DataContractJsonSerializer(typeof(List<Country>));

                            // Put ListOfcountryData in memoryStream
                            MemoryStream memoryStream = new MemoryStream();

                            // Use DCJS obj WriteObject Method for serializing
                            serListOfCountryData.WriteObject(memoryStream, ListOfCountryData);

                            // Initialize byte variable with memorystream ToArray method
                            byte[] data = memoryStream.ToArray();

                            // Initialize string with abstract data type encoding get string method
                            string utf8String = Encoding.UTF8.GetString(data, 0, data.Length);

                            // Write to file
                            StreamWriter streamWriter = new StreamWriter(FILENAME, false, Encoding.UTF8);
                            streamWriter.Write(utf8String);
                            streamWriter.Close();
                        }
                        break;
                    case 4: // Write List of Country to XML file
                        {
                            // User decides name of file to write too
                            Console.Write("\nEnter filename: ");
                            FILENAME = Console.ReadLine();

                            DataContractSerializer serListOfCountryData = new DataContractSerializer(typeof(List<Country>));
                            MemoryStream memoryStream = new MemoryStream();
                            serListOfCountryData.WriteObject(memoryStream, ListOfCountryData);
                            byte[] data = memoryStream.ToArray();
                            string utf8String = Encoding.UTF8.GetString(data, 0, data.Length);

                            // Write to file
                            StreamWriter streamWriter = new StreamWriter(FILENAME, false, Encoding.UTF8);
                            streamWriter.Write(utf8String);
                            streamWriter.Close();
                        }
                        break;
                    case 5: // Display All Country List Items on Screen
                        {
                            foreach (Country c in ListOfCountryData)
                            {
                                Console.WriteLine(c);
                            }
                        }
                        break;
                    case 6: // Find and display country by name
                        {
                            string target;

                            Console.Write("\nEnter country name: ");
                            target = Console.ReadLine();

                            foreach(Country c in ListOfCountryData)
                            {
                                if(c.Name == target)
                                {
                                    Console.WriteLine(c);
                                }
                            }
                        }
                        break;
                    case 7: // Find and display countries that use a given currency code
                        {
                            string target;

                            Console.Write("\nEnter currency code: ");
                            target = Console.ReadLine();

                            Console.WriteLine();

                            foreach (Country c in ListOfCountryData)
                            {
                                foreach(Currency curr in c.Currencies)
                                {
                                    if(curr.Code == target)
                                    {
                                        Console.WriteLine(c.Name);
                                    }
                                }                       
                            }
                        }
                        break;
                    case 8: // Exit
                        {
                            // Show ending program message
                            Console.WriteLine("Ending program...");
                        }
                        break;
                    default:
                        Console.WriteLine("\nYour choice must be a number from 1-16.\n");
                        break;
                }
                
            }
            while (Convert.ToInt32(userChoice) != 8);
        }
        #endregion
    }
}
