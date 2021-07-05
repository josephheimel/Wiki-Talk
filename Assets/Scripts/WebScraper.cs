using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;

public class WebScraper : MonoBehaviour
{
    //Target for List of Words
    [SerializeField] private GameObject hintWordInstantiator;

    //Text file of 1000 most common english words
    [SerializeField] private TextAsset common;
    //Text file of 100,000 english words
    [SerializeField] private TextAsset dictionary;

    /*
     * This function takes all of the link text words on a wikipedia page and puts them in a list
     * 
     * @param keyWord: The name of the wikipedia page
     * @return       : List of link text words
     */
    public List<string> GetWords(string keyWord)
    {
        // Create string lists from text files
        List<string> commonWords = common.text.Split('\n').ToList();
        List<string> dictionaryWords = dictionary.text.Split('\n').ToList();

        // Load Webpage
        string[] text;
        using (WebClient webClient = new WebClient())
        {
            text = webClient.DownloadString("https://en.wikipedia.org/wiki/" + keyWord).Split(new string[] { "References" }, System.StringSplitOptions.None)[0].Split(new string[] { "/wiki/" }, System.StringSplitOptions.None);
        }

        // Clean Strings
        List<string> cleanWords = new List<string>();
        for(int i = 0; i < text.Length; i++)
        {
            text[i] = text[i].Split('"')[0];
            text[i] = text[i].Replace('_', ' ');
            text[i] = text[i].Replace('-', ' ');

            string temp = text[i];
            temp = Regex.Replace(temp, @"[ ]", "");
            if (temp.All(char.IsLetterOrDigit))
            {
                // Further split each word in a phrase into its own list element of main list
                string[] tempArr = text[i].Split(' ');
                foreach(string s in tempArr)
                {
                    cleanWords.Add(s);
                }
            }
        }


        //unique collection of words in array
        IEnumerable<string> distinctWords = cleanWords.Distinct();
        List<string> words = distinctWords.ToList();

        // Remove Keyword from list
        for(int i = 0; i < words.Count; i++)
        {
            if (words[i].Equals(keyWord, System.StringComparison.CurrentCultureIgnoreCase))
            {
                words.RemoveAt(i);
            }
        }

        foreach(string w in words)
        {
            Debug.Log(w);
        }

        return words;
    }

    /*
     * Placeholder for function to organize words in the list
     */
    List<string> OrganizeWords(List<string> unorganizedWords)
    {
        return unorganizedWords;
    }
}
