using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    // Hint Word Instantiator
    [SerializeField] GameObject hintWordInstantiator;
    // Partner's Hint Word Instantiator
    [SerializeField] GameObject otherHintWordInstantiator;
    // Win Checker
    [SerializeField] GameObject winChecker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MouseDragBehaviour>().passedTextObject)
        {
            return;
        }

        if (collision.gameObject.GetComponent<MouseDragBehaviour>().colliderActive)
        {
            if (gameObject.name == "Black Hole Collider")
            {
                otherHintWordInstantiator.GetComponent<HintWordInstantiator>().Pass(collision.gameObject);
                hintWordInstantiator.GetComponent<HintWordInstantiator>().Decrement();
            }

            if (gameObject.name == "Navigate Box")
            {
                hintWordInstantiator.GetComponent<HintWordInstantiator>().Navigate(collision.gameObject);
                winChecker.GetComponent<AnswerChecker>().CheckAnswer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<MouseDragBehaviour>().colliderActive = true;
    }
}
