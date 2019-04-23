using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    private List<CardView> cardList = new List<CardView>();

    public bool IsEmpty => cardList.Count <= 0;

    public void AddCard(CardView card)
    {
        var pos = transform.position;
        pos.z -= 0.01f;
        card.transform.position = pos;

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
