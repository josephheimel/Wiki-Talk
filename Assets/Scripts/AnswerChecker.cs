using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class AnswerChecker : MonoBehaviour
{
    // Player One
    // Player One Current Page
    public GameObject p1;
    // Player One Previous Pages
    public List<string> p1History = new List<string>();
    // Player One Win Condition Text
    public GameObject p1WinText;
    // Player One Win Condition Button
    public GameObject p1WinButton;

    // Player Two
    // Player Two Current Page
    public GameObject p2;
    // Player Two Previous Pages
    public List<string> p2History = new List<string>();
    // Player Two Win Condition Text
    public GameObject p2WinText;
    // Player Two Win Condition Button
    public GameObject p2WinButton;


    public void CheckAnswer()
    {
        string p1Page = p1.GetComponent<TextMeshProUGUI>().text;
        string p2Page = p2.GetComponent<TextMeshProUGUI>().text;

        p1History.Add(p1Page);
        p2History.Add(p2Page);

        bool p1SoloWin = false;
        bool p2SoloWin = false;

        // Reset Data
        p1WinText.GetComponent<TextMeshProUGUI>().text = "Not Met";
        p2WinText.GetComponent<TextMeshProUGUI>().text = "Not Met";
        p1WinButton.GetComponent<WinButton>().p1Win = false;
        p2WinButton.GetComponent<WinButton>().p2Win = false;

        // Check page histories
        foreach (string s in p2History)
        {
            if (s.Equals(p1Page))
            {
                p1SoloWin = true;
            }
        }
        foreach (string s in p1History)
        {
            if (s.Equals(p2Page))
            {
                p2SoloWin = true;
            }
        }

        // Team Win
        if (p1Page == p2Page)
        {
            p1WinText.GetComponent<TextMeshProUGUI>().text = "Click for team win";
            p1WinButton.GetComponent<WinButton>().teamWin = true;
            p2WinText.GetComponent<TextMeshProUGUI>().text = "Click for team win";
            p2WinButton.GetComponent<WinButton>().teamWin = true;

            return;
        }
        p1WinButton.GetComponent<WinButton>().teamWin = false;
        p2WinButton.GetComponent<WinButton>().teamWin = false;
        // P1 Solo Win 
        if (p1SoloWin)
        {
            p1WinText.GetComponent<TextMeshProUGUI>().text = "Click for solo win";
            p1WinButton.GetComponent<WinButton>().p1Win = true;
        }

        // P2 Solo Win
        if (p2SoloWin)
        {
            p2WinText.GetComponent<TextMeshProUGUI>().text = "Click for solo win";
            p2WinButton.GetComponent<WinButton>().p2Win = true;
        }

        if(p1SoloWin && p2SoloWin)
        {
            p1WinText.GetComponent<TextMeshProUGUI>().text = "Page Stalemate";
            p2WinText.GetComponent<TextMeshProUGUI>().text = "Page Stalemate";
            p1WinButton.GetComponent<WinButton>().p1Win = false;
            p2WinButton.GetComponent<WinButton>().p2Win = false;
        }
    }
}
