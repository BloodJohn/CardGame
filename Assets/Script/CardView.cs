using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public TextMeshPro descriptionText;

    private SlotView currecntSlot;

    public void OnSlot(SlotView slot)
    {
        currecntSlot = slot;
    }

    public void OnDrag()
    {
        currecntSlot = null;
    }
}
