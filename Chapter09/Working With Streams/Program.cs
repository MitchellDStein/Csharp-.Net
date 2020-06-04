using System;
using System.IO;
using System.Xml;
using System.IO.Compression;        // used for compression
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
            WorkWithCompression();
            WorkWithCompression(useBrotli: false);
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

                xml.Close();                        // close helper and stream
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

        static void WorkWithCompression(bool useBrotli = true)
        {
            string fileExt = useBrotli ? "brotli" : "gzip";     // add check for using brotili or gzip compression

            // compress the XML output
            string filePath = Combine(CurrentDirectory, $"streams.{fileExt}");
            FileStream file = File.Create(filePath);

            Stream compressor;
            if (useBrotli)
            {
                compressor = new BrotliStream(file, CompressionMode.Compress);
            }
            else
            {
                compressor = new GZipStream(file, CompressionMode.Compress);
            }

            using (compressor)
            {
                using (XmlWriter xmlGzip = XmlWriter.Create(compressor))
                {
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");
                    foreach (string item in callsigns)
                    {
                        xmlGzip.WriteElementString("callsign", item);
                    }
                }
            }

            // output all the contents of the compressed file
            WriteLine("\n{0} contains {1:N0} bytes.", filePath, new FileInfo(filePath).Length);

            // read a compressed file
            WriteLine($"The compressed contents:");
            WriteLine(File.ReadAllText(filePath));

            WriteLine("\nReading the compressed XML file:");
            file = File.Open(filePath, FileMode.Open);

            Stream decompressor;
            if (useBrotli)
            {
                decompressor = new BrotliStream(file, CompressionMode.Decompress);
            }
            else
            {
                decompressor = new GZipStream(
                file, CompressionMode.Decompress);
            }

            using (decompressor)
            {
                using (XmlReader reader = XmlReader.Create(decompressor))
                {
                    while (reader.Read()) // read the next XML node
                    {
                        // while reading, check if we are on an element node named callsign
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                        {
                            reader.Read();                  // move to the text inside element
                            WriteLine($"{reader.Value}");   // read its value
                        }
                    }
                }
            }
        }
    }
}
