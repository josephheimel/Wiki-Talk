using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class HintWordInstantiator : MonoBehaviour
{
    // File to read words from
    public TextAsset topicList;
    // Number of hint word objects to create
    public int numberOfWords;
    // Manual correct answer to current puzzle
    public string answer;

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

    private Queue<string> hintWords;

    private void Start()
    {
        InstantiateHintWords();
    }

    void InstantiateHintWords()
    {
        // Get Answer
        if (answer.Equals(""))
        {
            string[] pages = topicList.text.Split('\n');
            answer = pages[Random.Range(0,pages.Length-1)];
        }

        // Set Answer
        answerChecker.GetComponent<AnswerChecker>().fileName = answer;


        // Get hint word list using function call
        List<string> temp = webScraper.GetComponent<WebScraper>().GetWords(answer);
        temp.Shuffle();
        hintWords = new Queue<string>(temp);


        // Create Word Objects
        for (int i = 0; i < numberOfWords; i++)
        {
            CreateTextObject();
        }
    }

    private void CreateTextObject()
    {
        DistanceJoint2D tempJoint;
        // Instantiate at the Send/Recieve object and zero rotation.
        GameObject instantiatedObject = Instantiate(prefab, center.position, Quaternion.identity);
        // Set Parent
        instantiatedObject.transform.SetParent(canvasTransform);
        // Set Anchor
        tempJoint = instantiatedObject.GetComponent<DistanceJoint2D>();
        tempJoint.connectedBody = textAnchor;
        tempJoint.distance = Random.Range(250, 400);
        // Nudge in random direction
        Vector2 randomDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        instantiatedObject.transform.Translate(randomDirection);
        // Set text
        instantiatedObject.GetComponent<TextMeshProUGUI>().text = hintWords.Dequeue();
        // Attach Word Bank Manager
        instantiatedObject.GetComponent<MouseDragBehaviour>().wordBank = wordBank;
    }

    public void OnBlackHoleTrigger(GameObject textObject)
    {
        // Enqueue old text
        hintWords.Enqueue(textObject.GetComponent<TextMeshProUGUI>().text);

        // Destroy old text
        Destroy(textObject);

        // Create new text
        CreateTextObject();
    }
}

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
