using System;
using System.Collections.Generic;

class Program
{
    class Example
    {
        List<string> values = new List<string>
    {
        {"www.example.com"},
        {"www.google.com" }
    };

        Dictionary<string, List<string>> _dict = new Dictionary<string, List<string>>();

        public List<string> GetURL(string portlet_id)
        {
            // Try to get the result in the Dictionary
            List<string> urlList;
            if (_dict.TryGetValue(portlet_id, out urlList))
            {
                return urlList;
            }
            else
            {
                return null;
            }
        }

        public void AddDataSource(string portlet_id, List<string> values, string newURL)
        {
            if (!(values.Contains(newURL)))
            {
                values.Add(newURL);
            }

            _dict.Add(portlet_id, values);
        }
    }
}