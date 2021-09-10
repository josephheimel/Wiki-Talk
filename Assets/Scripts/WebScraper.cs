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
            text = webClient.DownloadString("https://relatedwords.org/relatedto/" + keyWord).Split(new string[] { "\"terms\":[" }, StringSplitOptions.None)[1].Split(new string[] { "{\"word\":\"" }, 21, StringSplitOptions.None);
        }

        //// Clean Strings
        List<string> words = new List<string>();
        for (int i = 1; i < text.Length; i++)
        {
            //Isolate link text from rest of html
            text[i] = text[i].Split('"')[0];

            words.Add(text[i]);
        }

        return words;
    }
}
