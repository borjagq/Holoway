using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace holowayapi
{

    class GetQueryCreator
    {

       Dictionary<string, List<string> > values;

        public GetQueryCreator()
        {

            // We put an empty value to make sure that it's never null and we
            // can concat.
            values = new Dictionary<string, List<string> >(); 

        }

        public void Add(string name, string value)
        {

            // If the key exists, just add a new value.
            if (!values.ContainsKey(name))
            {
                values[name] = new List<string>();
            }

            values[name].Add(value);    

        }

        public override string ToString()
        {

            // Init the query text.
            string query = "";

            // Iterate through the values in the dictionary.
            foreach(KeyValuePair<string, List<string> > entry in values)
            {

                // If the string is not empty, we need to append a &.
                if (!String.IsNullOrEmpty(query))
                {
                    query += "&";
                }

                // Check if it's a single value, or nah.
                if (values[entry.Key].Count == 1) {

                    // Add the value.
                    query += entry.Key + "=" + UnityWebRequest.EscapeURL(entry.Value[0]);

                } else {

                    for (int i = 0; i < entry.Value.Count; i++)
                    {

                        // Don't add it on the first iteration.
                        if (i > 0)
                        {
                            query += "&";
                        }

                        // Add the value.
                        query += entry.Key + "[]=" + UnityWebRequest.EscapeURL(entry.Value[0]);
                        
                    }

                }

            }

            return query;

        }

    }

}
