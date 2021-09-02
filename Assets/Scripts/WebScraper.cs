using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;

public class WebScraper : MonoBehaviour
{
    /*
     * This function takes all of the link text words on a wikipedia page and puts them in a list
     * 
     * @param keyWord: The name of the wikipedia page
     * @return       : List of link text words
     */
    public List<string> GetWords(string keyWord)
    {
        // Load Webpage
        string[] text;
        using (WebClient webClient = new WebClient())
        {
            text = webClient.DownloadString("https://en.wikipedia.org/wiki/" + keyWord).Split(new string[] { "References" }, StringSplitOptions.None)[0].Split(new string[] { "/wiki/" }, StringSplitOptions.None);
        }

        // Clean Strings
        List<string> words = new List<string>();
        for(int i = 0; i < text.Length; i++)
        {
            //Isolate link text from rest of html
            text[i] = text[i].Split('"')[0];

            //Check if string is the page name
            if (text[i].Equals(keyWord, System.StringComparison.CurrentCultureIgnoreCase))
            {
                text[i] = "###";
            }


            string temp = text[i];
            temp = Regex.Replace(temp, @"[ ]", "");

            if (temp.All(char.IsLetterOrDigit))
            {
                words.Add(text[i]);
            }
        }

        //unique collection of words in array
        IEnumerable<string> distinctWords = words.Distinct();
        words = distinctWords.ToList();


        return words;
    }


    /*
     * Get Page Views
     */
    public int GetPageViews(string page)
    {
        using (WebClient webClient = new WebClient())
        {
           

            try
            {
                // Get count from html
                int count = Int32.Parse(webClient.DownloadString("https://wikimedia.org/api/rest_v1/metrics/pageviews/per-article/en.wikipedia/all-access/all-agents/" + page + "/monthly/2021070100/2021080100").Split(new string[] { "views\":" }, StringSplitOptions.None)[1].Split(new string[] { "}" }, StringSplitOptions.None)[0]);
                return count;
            } 
            catch(WebException)
            {

            }
        }

        return -1;
    }
}
