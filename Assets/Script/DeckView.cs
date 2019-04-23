using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckView : MonoBehaviour
{
    public CardView prefab;
    public Vector3 shift = new Vector3(0.1f, -0.1f, -0.1f);
    private List<CardView> cardList = new List<CardView>();

    private void Start()
    {
        CreateCard();
    }

    private void CreateCard()
    {
        for (var i = 0; i < 3; i++)
        {
            var card = Instantiate(prefab, transform);
            Discard(card);
        }
    }

    public CardView DealCard()
    {
        if (cardList.Count <= 0) return null;

        var result = cardList.Last();
        cardList.Remove(result);

        return result;
    }

    public void Discard(CardView card)
    {
        if (card==null) return;

        card.OnDrag(true);

        card.transform.parent = transform;
        card.transform.localRotation = Quaternion.identity;
        card.transform.localPosition = shift * cardList.Count;

        cardList.Add(card);
    }
}
