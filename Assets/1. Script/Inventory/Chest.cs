using UnityEngine;

public class Chest : Interactable
{
    [Header("References")]
    [SerializeField] private Item rewardItem;
    [SerializeField] private GameObject openedChestPrefab;

    private void Awake()
    {
        if(openedChestPrefab == null)
        {
            Instantiate(openedChestPrefab,
            transform.position,
            transform.rotation);
        }
        openedChestPrefab.SetActive(false);
    }


    protected override void Interact()
    {
        Inventory.Instance.AddItem(rewardItem);
        AudioManager.Inst.PlayItemUseEffect(0);

        uiManager.HideInteractButton();

        openedChestPrefab.SetActive(true);

        WorldManager.Instance.RegisterVanishObject(openedChestPrefab);

        Destroy(gameObject);
    }
}