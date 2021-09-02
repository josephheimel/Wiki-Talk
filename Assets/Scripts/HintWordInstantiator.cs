using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.IO;

public class HintWordInstantiator : MonoBehaviour
{
    // File to read words from
    public TextAsset topicList;
    // Number of link word objects to create
    public int numberOfWords;
    // Manual name of current page
    public string pageName;
    // Page Name Display in Game
    public GameObject pageText;

    // Web Scraper - gets list of hint words
    [SerializeField] private GameObject webScraper;
    // Hint Word Prefab
    [SerializeField] private GameObject prefab;
    // Location to spawn prefab
    [SerializeField] private Transform center;
    // Parent for text objects
    [SerializeField] private GameObject textObjectParent;
    // Position anchor for text objects
    [SerializeField] private Rigidbody2D textAnchor;

    // Queue of words to make
    private Queue<string> wordQ = new Queue<string>();
    // List of words
    private List<string> words = new List<string>();



    private void Start()
    {
        SetUp();
    }

    private void Update()
    {
        if (wordQ.Count > 0)
        {
            CreateTextObject(wordQ.Dequeue(), false);
        }
    }


    void SetUp()
    {
        // Get Page Name
        if (pageName.Equals(""))
        {
            string[] pages = topicList.text.Split('\n');
            pageName = pages[Random.Range(0, pages.Length - 1)];
        }


        // Set Page Text
        pageText.GetComponent<TextMeshProUGUI>().text = pageName;

        // Get hint word list using function call
        words = webScraper.GetComponent<WebScraper>().GetWords(pageName);

        // Sort
        StartCoroutine("Sort");
    }


    private IEnumerator Sort()
    {
        List<string> organizedWords = new List<string>();
        Dictionary<string, int> pageViews = new Dictionary<string, int>();

        foreach (string s in words)
        {
            int count = webScraper.GetComponent<WebScraper>().GetPageViews(s);

            if (count > 0)
            {
                pageViews.Add(s, count);
            }
        }

        int i = 0;

        foreach (KeyValuePair<string, int> pair in pageViews.OrderByDescending(key => key.Value))
        {
            if (i >= numberOfWords)
            {
                break;
            }
            i++;

            // Add to list and remove underscores
            wordQ.Enqueue(pair.Key);
        }

        yield return null;
    }
    

    /*
     * @param          text: Pagename to create a text object for
     * @param borderEnabled: Toggle for visible border to distinguish passed words
     */
    private void CreateTextObject(string text, bool borderEnabled)
    {
        DistanceJoint2D tempJoint;
        // Instantiate at the Send/Recieve object and zero rotation.
        GameObject instantiatedObject = Instantiate(prefab, center.position, Quaternion.identity);
        // Set Parent
        instantiatedObject.transform.SetParent(textObjectParent.transform);
        // Set Anchor
        tempJoint = instantiatedObject.GetComponent<DistanceJoint2D>();
        tempJoint.connectedBody = textAnchor;
        tempJoint.distance = Random.Range(250, 400);
        // Nudge in random direction
        Vector2 randomDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        instantiatedObject.transform.Translate(randomDirection);
        // Set text
        TextMeshProUGUI textUI = instantiatedObject.GetComponent<TextMeshProUGUI>();

        textUI.text = text;

        if (borderEnabled)
        {
            instantiatedObject.GetComponent<MouseDragBehaviour>().passedTextObject = true;
            textUI.fontStyle = FontStyles.Italic;
        }
    }

    public void Pass(GameObject textObject)
    {
        // Create new text
        CreateTextObject(textObject.GetComponent<TextMeshProUGUI>().text, true);

        // Change word count
        numberOfWords++;

        // Destroy old text
        Destroy(textObject);
    }   

    public void Decrement()
    {
        numberOfWords--;
    }


    public void Navigate(GameObject textObject)
    {
        // Clean
        Clean();

        // Change Page Name and call instantiator
        pageName = textObject.GetComponent<TextMeshProUGUI>().text;
        SetUp();
    }

    private void Clean()
    {
        //empty queue and destroy old objects
        StopCoroutine("Sort");
        wordQ.Clear();

        foreach (Transform child in textObjectParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void TeamWin()
    {
        Clean();
        pageText.GetComponent<TextMeshProUGUI>().text = "Victory!";
        wordQ.Enqueue("We");
        wordQ.Enqueue("won");
    }

    public void Win()
    {
        Clean();
        pageText.GetComponent<TextMeshProUGUI>().text = "Victory!";
        wordQ.Enqueue("You");
        wordQ.Enqueue("won");
    }

    public void Lose()
    {
        Clean();
        pageText.GetComponent<TextMeshProUGUI>().text = "Defeat!";
        wordQ.Enqueue("You");
        wordQ.Enqueue("lost");
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
