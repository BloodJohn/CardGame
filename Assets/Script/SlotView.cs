using System.Collections.Generic;
using UnityEngine;

public class SlotView : MonoBehaviour
{
    private List<CardView> cardList = new List<CardView>();
    
    public void AddCard(CardView card)
    {
        var pos = transform.position;

        pos.z -= 0.01f;

        card.transform.position = pos;
    }
}
