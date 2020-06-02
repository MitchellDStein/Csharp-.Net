﻿using System;
using System.IO;
using System.Xml;
using static System.Console;
using static System.Environment;
using static System.IO.Path;

namespace Working_With_Streams
{
    class Program
    {
        static string[] callsigns = new string[] {
            "Husker", "Starbuck", "Apollo", "Boomer",
            "Bulldog", "Athena", "Helo", "Racetrack" };

        static void Main(string[] args)
        {
            // WorkingWithText();
            WorkingWithXml();
        }

        static void WorkingWithText()
        {
            string textFile = Combine(CurrentDirectory, "streams.txt"); // define a file to write to
            StreamWriter text = File.CreateText(textFile);              // create a text file and return a helper writer
            foreach (string item in callsigns)                          // enumerate the strings, writing each one to the stream on a new line
            {
                text.WriteLine(item);
            }
            text.Close();   // release resources

            WriteLine("{0} contains {1:N0} bytes.", textFile, new FileInfo(textFile).Length);   // output the contents of the file
            WriteLine(File.ReadAllText(textFile));
        }

        static void WorkingWithXml()
        {
            FileStream xmlFileStream = null;
            XmlWriter xml = null;
            try
            {
                string xmlFile = Combine(CurrentDirectory, "streams.xml");                                  // define file to write to
                xmlFileStream = File.Create(xmlFile);                                            // create a file stream
                xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });   // wrap file stream in XML writer helper & auto indent nested elements

                xml.WriteStartDocument();           // write the XML declaration
                xml.WriteStartElement("callsigns"); // write a root element
                foreach (string item in callsigns)  // enumerate the strings, writing each one to the stream
                {
                    xml.WriteElementString("callsign", item);
                }
                xml.WriteEndElement();              // write and close root element

                xml.Close();            // close helper and stream
                xmlFileStream.Close();

                WriteLine("{0} contains {1:N0} bytes.", xmlFile, new FileInfo(xmlFile).Length);
                WriteLine(File.ReadAllText(xmlFile));
            }
            catch (Exception ex)
            {
                //if the path does not exist
                WriteLine($"{ex.GetType()} says {ex.Message}.");
            }
            finally
            {
                if (xml != null)
                {
                    xml.Dispose();
                    WriteLine("The XML writer's unmanaged resources have been disposed.");
                }
                if (xmlFileStream != null)
                {
                    xmlFileStream.Dispose();
                    WriteLine("The file stream's unmanaged resources have been disposed.");
                }
            }
        }
    }
}
