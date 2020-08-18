using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slotPrefab;

    public const int numSlots = 2;

    Image[] itemImages = new Image[numSlots];
    Item[] items = new Item[numSlots];
    GameObject[] slots = new GameObject[numSlots];

    public void Start()
    {
        CreateSlots();
    }

    public void CreateSlots()
    {
        if (slotPrefab != null)
        {
            for (int i = 0; i < numSlots; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = newSlot;
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            print("못들어옴");
            if (items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)
            {
                print("들어옴");
                items[i].quantity = items[i].quantity + 1;
                Slot slotScript = slots[i].GetComponent<Slot>();
                Text quantityText = slotScript.qtyText;
                quantityText.enabled = true;
                quantityText.text = items[i].quantity.ToString();
                print(items[i].quantity);
                return true;
            }

            if (items[i] == null)
            {
                items[i] = Instantiate(itemToAdd);
                items[i].quantity = 1;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return true;
            }
        }
        return false;
    }

    public void UseItem(int num)
    {
        if(items[num] != null && items[num].quantity > 0)
        {
            items[num].quantity = items[num].quantity - 1;

            print("items[num].quantity" + items[num].quantity);
            if (items[num].itemType == Item.ItemType.POTION)
            {             
                ItemManager.Instance.ItemPotion();
            }

            else if (items[num].itemType == Item.ItemType.BUSH)
            {
                ItemManager.Instance.ItemBush();
            }

            Slot slotScript = slots[num].GetComponent<Slot>();
            Text quantityText = slotScript.qtyText;
            quantityText.enabled = true;
            quantityText.text = items[num].quantity.ToString();

        }

    }

    
}