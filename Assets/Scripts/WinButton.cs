using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinButton : MonoBehaviour
{
    // Hint Word Instantiator
    [SerializeField] GameObject hintWordInstantiator;
    // Partner's Hint Word Instantiator
    [SerializeField] GameObject otherHintWordInstantiator;

    // Team Win Condition Met
    public bool teamWin;
    // P1 Win Condition Met
    public bool p1Win;
    // P1 Win Condition Met
    public bool p2Win;

    // Handle Win Conditions
    private void Update()
    {
        if (gameObject.name == "Win Condition Box")
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (GetComponent<BoxCollider2D>().OverlapPoint(mousePosition))
                {
                    if (teamWin)
                    {
                        hintWordInstantiator.GetComponent<HintWordInstantiator>().TeamWin();
                        otherHintWordInstantiator.GetComponent<HintWordInstantiator>().TeamWin();
                    }
                    if (p1Win)
                    {
                        hintWordInstantiator.GetComponent<HintWordInstantiator>().Win();
                        otherHintWordInstantiator.GetComponent<HintWordInstantiator>().Lose();
                    }
                    if (p2Win)
                    {
                        hintWordInstantiator.GetComponent<HintWordInstantiator>().Win();
                        otherHintWordInstantiator.GetComponent<HintWordInstantiator>().Lose();
                    }
                }
            }
        }
    }
}
