using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPGItems;
public class UIItem : Button
{
    public Image itemImage;
    public Item item;

    void SetItemImage()
    {
        itemImage = GetComponentsInChildren<Image>()[1];
        Debug.Assert(itemImage != null);
    }

    public Image GetItemImage()
    {
        if (itemImage == null)
        {
            SetItemImage();
        }
        return itemImage;
    }
    new void Awake()
    {
        base.Awake();
        SetItemImage();
    }

    new void Start()
    {
        base.Start(); 
    }
    public void RefreshImage()
    {
        SetItemImage();
        Color newColor = Color.white;

        if (item == null)
        {
            itemImage.sprite = null;
            newColor.a = 0f;     
        }
        else
        {
            itemImage.sprite = item.sprite;
            newColor.a = 1f;
        }
        itemImage.color = newColor;
    }
    public void SetItem(Item i)
    {
        this.item = i;
    }

    public void Click()
    {
        if (!SaveManager.inputEnabled)
        {
            return;
        }

        if (item != null)
            item.Use();
        else
            Debug.LogError("There is no item");
    }

    public void ShowItemDialog()
    {

    }

    public void HideItemDialog()
    {

    }
    void Update()
    {
        
    }

    public bool isEmpty()
    {
        return item == null;
    }

    
}
