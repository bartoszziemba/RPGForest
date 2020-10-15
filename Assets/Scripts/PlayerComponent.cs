using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGItems;
using RPGNamespace;
using Equipment = System.Collections.Generic.Dictionary<RPGItems.EquipableItem.EquipementType, RPGItems.EquipableItem>;

public class PlayerComponent : MonoBehaviour
{

    Inventory inventory = new Inventory(12);
    PlayerMovement playerMovement;
    Equipment equipment = new Equipment();
    UIManager uiManager;

    string heroName = "Arnold";
    int baseArmor = 2;
    int baseDamage = 2;

    int armor;
    int damage;
    int health = 200;
    int maxHp = 200;

    float glowTime;
    bool inBush;


    FighterInfo playerFighterInfo = new FighterInfo();

    void Awake()
    {
        InitializeEquipement();
        uiManager = GetComponent<UIManager>();
        playerMovement = GetComponent<PlayerMovement>();
        if (!uiManager)
            Debug.LogError("NO UI MANAGER FOUND");
        
    }
    IEnumerator EncounterCorutine()
    {
        while (true)
        {
            if(inBush && !playerMovement.isIdle() && SaveManager.inputEnabled)
            {
                int roll = Random.Range(0, 200);
                Debug.Log($"Rolled {roll}");
                if (roll > 190)
                    StartRandomEncounter();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void StartRandomEncounter()
    {
        int roll = Random.Range(0, 2);
        FighterInfo enemy;
        switch (roll)
        {
            case 0:
                enemy = new FighterInfo(HardcodedEnemies.oversisedWurm);
                break;
            case 1:
                enemy = new FighterInfo(HardcodedEnemies.fatRat);
                break;
            case 2:
                enemy = new FighterInfo(HardcodedEnemies.smallWasp);
                break;
            default:
                enemy = new FighterInfo(HardcodedEnemies.oversisedWurm);
                break;
        }
        uiManager.ShowImportantMessage($"Encountered {enemy.name}!", () => StartEncounter(enemy));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && SaveManager.inputEnabled)
        {
            StartRandomEncounter();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(new UsableItem(HardcodedItems.items["hpotion"] as UsableItem));
        }
    }
    void InitializeEquipement()
    {
        if(!equipment.ContainsKey(EquipableItem.EquipementType.Boots))
        {
            equipment.Add(EquipableItem.EquipementType.Boots, null);
        }
        if (!equipment.ContainsKey(EquipableItem.EquipementType.Weapon))
        {
            equipment.Add(EquipableItem.EquipementType.Weapon, null);
        }
        if (!equipment.ContainsKey(EquipableItem.EquipementType.Chestplate))
        {
            equipment.Add(EquipableItem.EquipementType.Chestplate, null);
        }
        if (!equipment.ContainsKey(EquipableItem.EquipementType.Helmet))
        {
            equipment.Add(EquipableItem.EquipementType.Helmet, null);
        }

    }
    void Start()
    {

        //Deserialize(HardcodedPlayers.testPlayer);
        
        RefreshEquipment();
        StartCoroutine(EncounterCorutine());

    }
    void CalculateEquipementBonus()
    {
        int armor = baseArmor;
        int strength = baseDamage;
        foreach (var pair in equipment)
        {
            if (pair.Value != null)
            {
                armor += pair.Value.armor;
                strength += pair.Value.strength;
            }
        }
        this.armor = armor;
        this.damage = strength;
    }
    public void RefreshEquipment()
    {
        CalculateEquipementBonus();
        uiManager.RefreshEquipmentUI(equipment, armor, damage);

    }

    public bool AddItem(Item it)
    {
        if (!inventory.AddItem(it))
            return false;

        it.SetOwner(this);
        uiManager.RefreshInventoryUI(inventory);
        return true;
    }
    public void RemoveItem(int index)
    {
        if (index != -1)
        {
            inventory.RemoveItem(index);
            uiManager.RefreshInventoryUI(inventory);
        }
    }



    public void Equip(EquipableItem it)
    {
        print($"{it.type}");
        if (equipment[it.type] == null)
        {
            equipment[it.type] = it;
            RemoveItem(it.inventoryPlace);
        }
        else
        {
            RemoveItem(it.inventoryPlace);
            equipment[it.type].equiped = false;
            AddItem(equipment[it.type]);
            equipment[it.type] = it;
        }
        it.equiped = true;
        RefreshEquipment();
        uiManager.RefreshInventoryUI(inventory);

    }
    public void UnEquip(EquipableItem it)
    {

        if (AddItem(it))
        {
            equipment[it.type] = null;
            it.equiped = false;
            RefreshEquipment();
        }


    }
    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHp);
        RefreshHeroInfo();
    }

    public void Use(UsableItem it)
    {
        print($"Used item: {it.name}");
        glowTime += it.glowTime;
        Heal(it.healthAmount);
        RemoveItem(it.inventoryPlace);
    }

    public void StartEncounter(FighterInfo enemy)
    {
        GameObject.FindGameObjectWithTag("EncounterManager").GetComponent<EncounterManger>().player = ToFighterInfo();
        GameObject.FindGameObjectWithTag("EncounterManager").GetComponent<EncounterManger>().FightAgainst(enemy);
    }

    public FighterInfo ToFighterInfo()
    {
        FighterInfo fighterInfo = new FighterInfo(heroName, health, maxHp, this.damage, this.armor);

        return fighterInfo;
    }

    public string Serialize()
    {
        Misc.SerializableVector3 playerPos = transform.position;
        return health.ToString() + ';' + maxHp.ToString() +';'+ heroName + ';' + '\n'
            +  SerializeEquipement() + '\n'
            + inventory.Serialize() + '\n'
            + playerPos.ToString();
    }

    public void Deserialize(string repr)
    {
             
        try
        {
            string[] tab = repr.Split('\n');
            string[] tabHp = tab[0].Split(';');
            health = int.Parse(tabHp[0]);
            print("health OK");
            maxHp = int.Parse(tabHp[1]);
            print("maxHp OK");
            heroName = tabHp[2];

            DeserializeEquipement(tab[1]);
            print("Equipement OK");
            inventory.Deserialize(tab[2], this);
            print("inventory OK");
            transform.position = new Misc.SerializableVector3(tab[3]);
            print("position OK");
        }
        catch(System.Exception e)
        {
            Debug.LogError("Serialized player is corrupted " + e.Message);
        }
        RefreshHeroInfo();

        uiManager.RefreshInventoryUI(inventory);

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
    void RefreshHeroInfo()
    {
        uiManager.SetHealthText(health, maxHp);
        uiManager.SetHeroName(heroName);
    }
    string SerializeEquipement()
    {
        string result = "eq;";
        
        foreach(var pair in equipment)
        {
            if (pair.Value != null)
                result += pair.Value.ToString();
            result += ';';
        }
        return result;
    }
    void DeserializeEquipement(string repr)
    {
        print("starting deseriallize eq");
        InitializeEquipement();
        print("eq initialized ");

        string[] items = repr.Split(';');
        repr = repr.TrimEnd(';');
        
        for(int i = 1;i<items.Length;i++)
        {
            string itemstring = items[i];
            if (itemstring != "")
            {
                EquipableItem item = EquipableItem.FromString(itemstring.Trim());
                item.SetOwner(this);
                Equip(item);
            }

        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Encounters")
            inBush = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Encounters")
            inBush = false;
    }

}
