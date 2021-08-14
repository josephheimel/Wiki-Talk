using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class AnswerChecker : MonoBehaviour
{
    // Text Entry Field
    public string inputField;
    // Text file
    public string fileName;
    // Whether correct answer is found
    public bool correctAnswer;
    // Toggle for white text on correct answer
    [SerializeField] private bool highlightOnCorrectAnswer;


    private void Update()
    {
        //Strip Characters (period, comma, underscore, dash, apostrophy, space)
        inputField = Regex.Replace(inputField, @"[.,_\-' ]", "");
        fileName = Regex.Replace(fileName, @"[.,_\-' ]", "");

        if (inputField.Equals(fileName,StringComparison.OrdinalIgnoreCase))
        {
            correctAnswer = true;

            if (highlightOnCorrectAnswer)
            {
                //GetComponent<RawImage>().color = Color.white;
            }
        } else {
            correctAnswer = false;
            //GetComponent<RawImage>().color = Color.gray;
        }
    }
}
