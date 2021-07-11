using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    // Hint Word Instantiator
    [SerializeField] GameObject hintWordInstantiator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MouseDragBehaviour>().colliderActive)
        {
            hintWordInstantiator.GetComponent<HintWordInstantiator>().OnBlackHoleTrigger(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<MouseDragBehaviour>().colliderActive = true;
    }


    //              Word Bank Code
    /*    // Y Offset
    public float y_off;
    // Number of this field
    [SerializeField] int num;
    // Bank manager object
    [SerializeField] GameObject wordBank;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Disable colliders
        collision.gameObject.GetComponent<DistanceJoint2D>().enabled = false;
        collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        // Center text
        Vector3 offset = new Vector3(0, y_off, 0);
        collision.gameObject.transform.position = transform.position + offset;

        // Don't take more text / Register text
        wordBank.GetComponent<WordBankManager>().RegisterWord(collision.gameObject, num);
    }
    */
}
