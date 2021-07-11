using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 lastMousePosition;

    [SerializeField] private DistanceJoint2D joint;
    [SerializeField] private CircleCollider2D circle;
    [SerializeField] private RectTransform rect;
    public GameObject wordBank;
    public bool colliderActive = false;

    /// This method will be called on the start of the mouse drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (joint.enabled)
        {
            joint.enabled = false;
            circle.enabled = false;
        } else {
            wordBank.GetComponent<WordBankManager>().DeregisterWord(gameObject);
        }

        lastMousePosition = eventData.position;
    }

    /// This method will be called during the mouse drag
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPosition = Vector2.zero;

        // Adjust Camera coordinates to local space coordinates
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, Camera.main, out localPosition);

        rect.position = rect.TransformPoint(localPosition);
    }

    /// This method will be called at the end of mouse drag
    public void OnEndDrag(PointerEventData eventData)
    {
        joint.enabled = true;
        circle.enabled = true;
    }
}