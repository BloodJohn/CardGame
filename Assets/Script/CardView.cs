using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public TextMeshPro damageText;
    public TextMeshPro helthText;
    public TextMeshPro nameText;
    public TextMeshPro descriptionText;
    public MeshRenderer imageQuad;


    public bool IsPlayerTable;

    private SlotView currecntSlot;

    public bool IsDraggable => currecntSlot != null;

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
