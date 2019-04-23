using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Camera mainCamera;

    private CardView currentCard;
    private Vector3 startPos;
    private Vector3 startShift;

    private List<SlotView> slotList = new List<SlotView>();

    private void Awake()
    {
        mainCamera = Camera.main;

        slotList.AddRange(FindObjectsOfType<SlotView>());
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
        else if (Input.GetMouseButtonUp(0))
        {
            StickCard(currentCard);
            currentCard = null;
        }

    }

    private CardView GetCard()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        foreach (var hit in Physics.RaycastAll(ray))
        {
            var card = hit.transform.gameObject.GetComponent<CardView>();

            if (card != null)
            {
                startPos = card.transform.position;
                startShift = hit.point - startPos;
                startShift.z = 0;
                card.OnDrag();
                return card;
            }
        }

        return null;
    }

    private void MoveCard(CardView card)
    {
        if (card==null) return;

        var clickPos = Input.mousePosition;
        //clickPos.z = mainCamera.nearClipPlane;
        clickPos.z = card.transform.position.z -  mainCamera.transform.position.z;
        var pos = mainCamera.ScreenToWorldPoint(clickPos, Camera.MonoOrStereoscopicEye.Mono);
        pos.z = startPos.z;

        card.transform.position = pos - startShift;
    }

    private void StickCard(CardView card)
    {
        if (card == null) return;
        var pos = card.transform.position;

        var bestSlot = slotList[0];
        var minDist2 = (bestSlot.transform.position - pos).sqrMagnitude;

        foreach (var slot in slotList)
        {
            var dist2 = (slot.transform.position - pos).sqrMagnitude;
            if (dist2>=minDist2) continue;
            minDist2 = dist2;
            bestSlot = slot;
        }

        bestSlot.AddCard(card);
    }
}
