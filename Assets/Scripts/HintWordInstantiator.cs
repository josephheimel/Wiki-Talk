using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class HintWordInstantiator : MonoBehaviour
{
    // File to read words from
    //public TextAsset textFile;

    //Correct answer to current puzzle
    public string answer;
    // Number of hint word objects to create
    public int numberOfWords;

    // Web Scraper - gets list of hint words
    [SerializeField] private GameObject webScraper;
    // Answer Checker
    [SerializeField] private GameObject answerChecker;
    // Word Bank Object
    [SerializeField] private GameObject wordBank;
    // Hint Word Prefab
    [SerializeField] private GameObject prefab;
    // Location to spawn prefab
    [SerializeField] private Transform center;
    // Transform for the canvas
    [SerializeField] private Transform canvasTransform;
    // Position anchor for text objects
    [SerializeField] private Rigidbody2D textAnchor;
    

    private void Start()
    {
        InstantiateHintWords();
    }

    void InstantiateHintWords()
    {
        // Set Answer
        answerChecker.GetComponent<AnswerChecker>().fileName = answer;


        // Get hint word list using function call
        List<string> allHintWords = webScraper.GetComponent<WebScraper>().GetWords(answer);
        List<string> hintWords = new List<string>();

        // Add a number of random words from list
        for(int i = 0; i < numberOfWords; i++)
        {
            if(allHintWords.Count > 0)
            {
                int index = Random.Range(0, allHintWords.Count - 1);
                hintWords.Add(allHintWords[index]);
                allHintWords.RemoveAt(index);
            }
        }

        // Create Word Objects
        DistanceJoint2D tempJoint;

        foreach(string word in hintWords)
        {
            Debug.Log(word);


            // Instantiate at the Send/Recieve object and zero rotation.
            GameObject instantiatedObject = Instantiate(prefab, center.position, Quaternion.identity);
            // Set Parent
            instantiatedObject.transform.SetParent(canvasTransform);
            // Set Anchor
            tempJoint = instantiatedObject.GetComponent<DistanceJoint2D>();
            tempJoint.connectedBody = textAnchor;
            tempJoint.distance = Random.Range(250, 500);
            // Nudge in random direction
            Vector2 randomDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            instantiatedObject.transform.Translate(randomDirection);
            // Set text
            instantiatedObject.GetComponent<TextMeshProUGUI>().text = word;
            // Attach Word Bank Manager
            instantiatedObject.GetComponent<MouseDragBehaviour>().wordBank = wordBank;
        }
    }
}
