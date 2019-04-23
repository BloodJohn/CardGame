using UnityEngine;

public class GameController : MonoBehaviour
{
    private Camera mainCamera;

    private CardItem currentCard;
    private Vector3 startPos;
    private Vector3 startShift;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        //AndroidInput.

        if (Input.GetMouseButtonDown(0))
        {
            currentCard = GetCard();
        }
        else if (Input.GetMouseButton(0))
        {
            MoveCard(currentCard);
        }

    }

    private CardItem GetCard()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        foreach (var hit in Physics.RaycastAll(ray))
        {
            var card = hit.transform.gameObject.GetComponent<CardItem>();

            if (card != null)
            {
                startPos = card.transform.position;
                startShift = hit.point - startPos;
                return card;
            }
        }

        return null;
    }

    private void MoveCard(CardItem card)
    {
        if (card==null) return;

        var clickPos = Input.mousePosition;
        //clickPos.z = mainCamera.nearClipPlane;
        clickPos.z = card.transform.position.z -  mainCamera.transform.position.z;
        var pos = mainCamera.ScreenToWorldPoint(clickPos, Camera.MonoOrStereoscopicEye.Mono);

        Debug.Log($"pos {pos} / {clickPos}");

        pos.z = startPos.z;

        card.transform.position = pos;
    }
}
