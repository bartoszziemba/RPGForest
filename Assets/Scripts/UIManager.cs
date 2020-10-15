using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPGItems;
using Equipment = System.Collections.Generic.Dictionary<RPGItems.EquipableItem.EquipementType, RPGItems.EquipableItem>;

public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;

    public UIItem[] uiItems;

    public UIItem UIHelmet;
    public UIItem UIWeapon;
    public UIItem UIChestplate;
    public UIItem UIBoots;

    public GameObject inventoryPanel;
    public GameObject characterPanel;
    public GameObject messagePanel;

    public Text messageText;
    public Text ArmorText;
    public Text DamageText;
    public Text HealthText;
    public Text HeroName;

    public Button closeMessageButton;

    void Awake()
    {
        uIManager = this;
    }

    public void RefreshInventoryUI(Inventory inventory)
    {
        for (int i = 0; i < inventory.ItemCount; i++)
        {
            uiItems[i].SetItem(inventory.GetItem(i));
            uiItems[i].RefreshImage();
        }
    }

    public void SetHeroName(string name)
    {
        HeroName.text = name;
    }
    public void SetHealthText(int health,int maxhealth)
    {
        HealthText.text = $"HP:{health}/{maxhealth}";
    }
    public void ShowImportantMessage(string message)
    {
        ShowImportantMessage(message, () => { });
    }
    public void ShowImportantMessage(string message,System.Action foo)
    {
        SaveManager.DisableInput();

        closeMessageButton.onClick.RemoveAllListeners();
        messageText.text = message;
        messagePanel.SetActive(true);

        closeMessageButton.onClick.AddListener( () => CloseMessageAndCall(foo) );
    }

    void CloseMessageAndCall(System.Action foo)
    {
        
        if (messagePanel.activeSelf == true)
        {
            messagePanel.SetActive(false);
        }
        foo();
        SaveManager.EnableInput();
    }

    void Update()
    {
        if (SaveManager.inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                characterPanel.SetActive(!characterPanel.activeSelf);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                ShowImportantMessage("Wiad!!", () => Debug.Log("It works"));
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseTopWindow();
            }
        }

    }
    void CloseTopWindow()
    {
        //if(messagePanel.activeSelf == true)
        //{
        //    messagePanel.SetActive(false);
        //    return;
        //}CANT BE HERE CAUSE OF FUNCTIon ATTACHED TO IT's CLOSE BUTTON
        if(characterPanel.activeSelf == true)
        {
            characterPanel.SetActive(false);
            return;
        }
        if(inventoryPanel.activeSelf == true)
        {
            inventoryPanel.SetActive(false);
            return;
        }

    }
    public void RefreshEquipmentUI(Equipment equipment,int armor,int damage)
    {
        Color color = Color.HSVToRGB(0, 0, 0.56f);
        color.a = 0.36f;

        ArmorText.text = $"arm: {armor}";
        DamageText.text = $"dmg: {damage}";

        UIHelmet.SetItem(equipment[EquipableItem.EquipementType.Helmet]);
        if (equipment[EquipableItem.EquipementType.Helmet] == null)
        {
            UIHelmet.GetItemImage().sprite = Resources.Load<Sprite>("ItemSprites/helmets");
            UIHelmet.GetItemImage().color = color;
        }
        else
        {
            UIHelmet.GetItemImage().sprite = equipment[EquipableItem.EquipementType.Helmet].sprite;
            UIHelmet.GetItemImage().color = Color.white;
        }

        UIChestplate.SetItem(equipment[EquipableItem.EquipementType.Chestplate]);
        if (equipment[EquipableItem.EquipementType.Chestplate] == null)
        {
            UIChestplate.GetItemImage().sprite = Resources.Load<Sprite>("ItemSprites/armor");
            UIChestplate.GetItemImage().color = color;
        }
        else
        {
            UIChestplate.GetItemImage().sprite = equipment[EquipableItem.EquipementType.Chestplate].sprite;
            UIChestplate.GetItemImage().color = Color.white;
        }

        UIBoots.SetItem(equipment[EquipableItem.EquipementType.Boots]);
        if (equipment[EquipableItem.EquipementType.Boots] == null)
        {
            UIBoots.GetItemImage().sprite = Resources.Load<Sprite>("ItemSprites/boots");
            UIBoots.GetItemImage().color = color;
        }
        else
        {
            UIBoots.GetItemImage().sprite = equipment[EquipableItem.EquipementType.Boots].sprite;
            UIBoots.GetItemImage().color = Color.white;
        }

        UIWeapon.SetItem(equipment[EquipableItem.EquipementType.Weapon]);
        if (equipment[EquipableItem.EquipementType.Weapon] == null)
        {
            UIWeapon.GetItemImage().sprite = Resources.Load<Sprite>("ItemSprites/sword");
            UIWeapon.GetItemImage().color = color;
        }
        else
        {
            UIWeapon.GetItemImage().sprite = equipment[EquipableItem.EquipementType.Weapon].sprite;
            UIWeapon.GetItemImage().color = Color.white;
        }


    }
}
