using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    private void Awake()
    {
        ClearSlot();
    }

    public void SetItem(Sprite sprite)
    {
        icon.sprite = sprite;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
    }
}