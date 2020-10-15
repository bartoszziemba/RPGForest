using RPGItems;
using System.Collections;

public class Inventory : IEnumerable
{
    private Item[] items;
    public int ItemCount { get { return items.Length; } private set { } }

    public Inventory(int Size)
    {
        items = new Item[Size];
    }

    public void Clear()
    {
        items = new Item[items.Length];
    }

    public string Serialize()
    {
        string result = "inv;";
        foreach (var item in this)
        {
            if (item != null)
                result += item.ToString();
            result += ';';
        }
        return result;
    }

    public void Deserialize(string repr, PlayerComponent owner)
    {
        this.Clear();
        int pos = 0;
        repr = repr.Trim(';');
        string[] savedItems = repr.Split(';');
        for (int i = 1; i < savedItems.Length; i++)
        {
            if (savedItems[i] != "")
            {
                if (HardcodedItems.items[savedItems[i]] is EquipableItem)
                {
                    items[pos] = new EquipableItem(HardcodedItems.items[savedItems[i]] as EquipableItem);
                }
                else
                {
                    items[pos] = new UsableItem((UsableItem)HardcodedItems.items[savedItems[i]]);
                }

                items[pos].SetOwner(owner);
                items[pos].inventoryPlace = pos;
            }
            pos++;
        }
    }

    public bool AddItem(Item newItem)
    {
        int spotIndex = FindSpot();

        if(newItem is EquipableItem)
        {
            newItem = newItem as EquipableItem;
        }
        if(newItem is UsableItem)
        {
            newItem = newItem as UsableItem;
        }

        if (spotIndex > -1)
        {
            newItem.inventoryPlace = spotIndex;
            items[spotIndex] = newItem;
            return true;
        }
        else
            return false;
    }

    public bool RemoveItem(int index)
    {
        if (items[index] != null)
        {
            items[index] = null;
            return true;
        }
        return false;
    }

    private int FindSpot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                return i;
        }
        return -1;
    }

    public Item GetItem(int index)
    {
        return items[index];
    }

    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)new InventoryEnumerator(items);
    }
}

public class InventoryEnumerator : IEnumerator
{
    public Item[] items;
    private int position = -1;

    public InventoryEnumerator(Item[] _items)
    {
        items = _items;
    }

    object IEnumerator.Current {
        get
        {
            return items[position];
        }
    }

    bool IEnumerator.MoveNext()
    {
        position++;
        return (position < items.Length);
    }

    void IEnumerator.Reset()
    {
        position = -1;
    }
}