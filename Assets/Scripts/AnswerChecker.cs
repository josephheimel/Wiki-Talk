using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerChecker : MonoBehaviour
{
    // Text Entry Field
    [SerializeField] private GameObject inputField;
    // Text file
    public string fileName;
    // Whether correct answer is found
    public bool correctAnswer;
    // Toggle for white text on correct answer
    [SerializeField] private bool revealCorrectAnswer;

    private void Update()
    {
        string text = inputField.GetComponent<TMP_InputField>().text;

        if(text.Equals(fileName,StringComparison.OrdinalIgnoreCase))
        {
            correctAnswer = true;

            if (revealCorrectAnswer)
            {
                GetComponent<RawImage>().color = Color.white;
            }
        } else {
            correctAnswer = false;
            GetComponent<RawImage>().color = Color.gray;
        }
    }
}
