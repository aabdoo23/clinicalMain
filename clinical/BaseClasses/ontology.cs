using System;
using System.Collections.Generic;
using System.IO;

namespace clinical.BaseClasses
{
    public class ontology
    {
        // Define a class to represent a term
        public class Term
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string Def { get; set; }
            public List<string> Urls { get; set; }
            public List<string> Synonyms { get; set; }
        }

        // Declare a variable to store the file name
        public string FileName { get; set; }

        public ontology()
        {
            // Assign the file name
            FileName = "C:\\Users\\polaalaa\\source\\repos\\clinicalMain\\New folder (3)\\clinical\\BaseClasses\\Terms.txt";
        }

        // A method to return the first 10 ontologies from the text file
        public List<Term> GetFirstTenOntologies()
        {
            // Declare a list to store the ontologies
            List<Term> ontologies = new List<Term>();

            // Declare a variable to store the current term
            Term currentTerm = null;

            // Declare a variable to store the current line
            string line;

            // Declare a variable to store the number of ontologies
            int ontologyCount = 0;

            // Use a stream reader to read the text file
            using (var sr = new StreamReader(FileName))
            {
                // Read the file line by line until the end or the ontology limit is reached
                while ((line = sr.ReadLine()) != null && ontologyCount < 10)
                {
                    // Check if the line starts with a term tag
                    if (line.StartsWith("[Term]"))
                    {
                        // If there is a previous term, add it to the list
                        if (currentTerm != null)
                        {
                            ontologies.Add(currentTerm);
                            ontologyCount++;
                        }

                        // Create a new term and initialize its properties
                        currentTerm = new Term();
                        currentTerm.Name = "";
                        currentTerm.Id = "";
                        currentTerm.Def = "";
                        currentTerm.Urls = new List<string>();
                        currentTerm.Synonyms = new List<string>();
                    }
                    // Check if the line starts with a name tag
                    else if (line.StartsWith("name: "))
                    {
                        // Extract the name value and assign it to the current term
                        currentTerm.Name = line.Substring(6);
                    }
                    // Check if the line starts with an id tag
                    else if (line.StartsWith("id: "))
                    {
                        // Extract the id value and assign it to the current term
                        currentTerm.Id = line.Substring(4);
                    }
                    // Check if the line starts with a def tag
                    else if (line.StartsWith("def: "))
                    {
                        // Extract the def value and assign it to the current term
                        currentTerm.Def = line.Substring(6, line.IndexOf('[') - 7);

                        // Extract the urls from the def value and add them to the current term
                        foreach (var url in line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - line.IndexOf('[') - 1).Split(','))
                        {
                            currentTerm.Urls.Add(url.Trim().Substring(4));
                        }
                    }
                    // Check if the line starts with a synonym tag
                    else if (line.StartsWith("synonym: "))
                    {
                        // Extract the synonym value and add it to the current term
                        currentTerm.Synonyms.Add(line.Substring(10, line.IndexOf('"', 11) - 10));
                    }
                }

                // If there is a last term, add it to the list
                if (currentTerm != null)
                {
                    ontologies.Add(currentTerm);
                    ontologyCount++;
                }
            }

            // Return the list of ontologies
            return ontologies;
        }
    }
}
