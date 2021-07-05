using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordBankManager : MonoBehaviour
{
    // Array of entry objects
    [SerializeField] private GameObject[] bankEntries = new GameObject[10];
    // Array of text objects
    private GameObject[] words = new GameObject[10];

    private void Awake()
    {
        for(int i = 0; i < words.Length; i++)
        {
            words[i] = null;
        }
    }

    public void RegisterWord(GameObject word, int placeNum)
    {
        words[placeNum] = word;
        bankEntries[placeNum].GetComponent<BoxCollider2D>().enabled = false;
        bankEntries[placeNum].GetComponent<RawImage>().color = Color.white;

        for (int i = 0; i < words.Length; i++)
        {
            if(i != placeNum & words[i] != null)
            {
                if (words[i].Equals(word))
                {
                    words[i] = null;
                    bankEntries[i].GetComponent<BoxCollider2D>().enabled = true;
                    bankEntries[i].GetComponent<RawImage>().color = Color.gray;
                }
            }
        }
    }

    public void DeregisterWord(GameObject word)
    {
        int placeNum = -1;
        for(int i = 0; i < words.Length; i++)
        {
            if (words[i] != null)
            {
                if (words[i].Equals(word))
                {
                    placeNum = i;
                }
            }
        }

        if (placeNum != -1)
        {
            words[placeNum] = null;
            bankEntries[placeNum].GetComponent<BoxCollider2D>().enabled = true;
            bankEntries[placeNum].GetComponent<RawImage>().color = Color.gray;
        }
    }
}
