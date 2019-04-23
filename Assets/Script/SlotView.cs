using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    public bool IsPlayerTable;

    private List<CardView> cardList = new List<CardView>();

    public bool IsEmpty => cardList.Count <= 0;
    
    public void AddCard(CardView card)
    {
        card.transform.parent = transform;
        card.transform.localRotation = Quaternion.identity;
        card.transform.localPosition = Vector3.zero + Vector3.back * 0.01f;

        card.OnSlot(this);
        cardList.Add(card);
    }

    public CardView ReleaseCard()
    {
        if (IsEmpty) return null;

        var result = cardList.Last();
        cardList.Remove(result);

        return result;
    }
}
