using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public DeckView PlayerDeck;
    public GameObject PlayerHand;
    public GameObject PlayerTable;

    public Button backBtn;
    public Button dealBtn;
    public Button turnBtn;

    private Camera mainCamera;

    private CardView currentCard;
    private Vector3 startPos;
    private Vector3 startShift;

    private List<CardView> cardList = new List<CardView>();

    private void Awake()
    {
        mainCamera = Camera.main;
        
        backBtn.onClick.AddListener(Discard);
        dealBtn.onClick.AddListener(DealCard);
    }


    private void Update()
    {
        //AndroidInput.

        if (Input.GetMouseButtonDown(0))
        {
            currentCard = PickCard();
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

    private void DealCard()
    {
        var card = PlayerDeck.DealCard();

        if (card != null)
        {
            foreach (var slot in PlayerHand.GetComponentsInChildren<SlotView>())
            {
                if (!slot.IsEmpty) continue;
                slot.AddCard(card);
                cardList.Add(card);
                return;
            }
        }

        PlayerDeck.Discard(card);
    }

    private void Discard()
    {
        foreach (var card in cardList)
        {
            PlayerDeck.Discard(card);
        }

        cardList.Clear();
    }


    private CardView PickCard()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        foreach (var hit in Physics.RaycastAll(ray))
        {
            var card = hit.transform.gameObject.GetComponent<CardView>();
            if (card == null) continue;
            if (!card.IsDraggable) continue;

            startPos = card.transform.position;
            startShift = hit.point - startPos;
            startShift.z = 0;
            card.OnDrag(false);
            return card;
        }

        return null;
    }

    private void MoveCard(CardView card)
    {
        if (card == null) return;

        var clickPos = Input.mousePosition;
        clickPos.z = card.transform.position.z - mainCamera.transform.position.z;
        var pos = mainCamera.ScreenToWorldPoint(clickPos, Camera.MonoOrStereoscopicEye.Mono);
        pos.z = startPos.z;

        card.transform.position = pos - startShift;
    }

    private void StickCard(CardView card)
    {
        if (card == null) return;
        var pos = card.transform.position;

        SlotView bestSlot = null;
        var minDist2 = float.MaxValue;

        foreach (var slot in FindObjectsOfType<SlotView>())
        {
            if (!slot.IsEmpty) continue;
            if (card.IsPlayerTable && !slot.IsPlayerTable) continue;

            var dist2 = (slot.transform.position - pos).sqrMagnitude;
            if (dist2 >= minDist2) continue;
            minDist2 = dist2;
            bestSlot = slot;
        }

        if (bestSlot != null) bestSlot.AddCard(card);
    }
}
