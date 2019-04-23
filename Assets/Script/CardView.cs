using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public TextMeshPro damageText;
    public TextMeshPro healthText;
    public TextMeshPro titleText;
    public TextMeshPro descriptionText;
    public MeshRenderer imageQuad;

    public bool IsPlayerTable;

    private SlotView currecntSlot;

    public bool IsDraggable => currecntSlot != null;


    public void InitCard(CardModel model)
    {
        titleText.text = model.name;
        descriptionText.text = model.description;
        damageText.text = model.damage.ToString();
        healthText.text = model.health.ToString();
    }


    public void OnSlot(SlotView slot)
    {
        currecntSlot = slot;
        IsPlayerTable = slot.IsPlayerTable;
    }

    //todo: не отпускать карту из слота, пока не нашли новый (IsPlayerTable)

    public void OnDrag(bool isDiscard)
    {
        if (currecntSlot != null)
            currecntSlot.ReleaseCard();
        currecntSlot = null;

        if (isDiscard) IsPlayerTable = false;
    }
}

public class CardModel
{
    public string name;
    public int damage;
    public int health;
    public string description;
    public Texture image;
}