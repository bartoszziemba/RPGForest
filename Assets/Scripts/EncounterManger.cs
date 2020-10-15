using RPGNamespace;
using RPGItems;

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Misc;
using System.Linq;
public class EncounterManger : MonoBehaviour
{
    public static EncounterManger encounterManger;
    public FighterInfo player;
    public FighterInfo enemy;

    
    private void Awake()
    {
        if (encounterManger != null)
            GameObject.Destroy(this.gameObject);
        else
            encounterManger = this;

        DontDestroyOnLoad(this);
    }

    public void FightAgainst(FighterInfo _enemy)
    {
        //SerializableVector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        PlayerComponent p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponent>();
        //PlayerPrefs.SetString("playerPos", playerPos.ToString());

        //PlayerPrefs.SetString("player", p.Serialize());
        SaveManager.SaveOnCurrentSlot(p.Serialize());

        //PlayerPrefs.Save();
        enemy = _enemy;
        SceneManager.LoadScene("_Encounter");
    }

    public void AttackEnemy()
    {
        Debug.Log($"{player} attacked {enemy}");
        player.Attack(enemy);
        enemy.Attack(player);
        Debug.Log($"Result: {player} , {enemy}");
    }

    public void CheckEndOfEncounter()
    {
        bool win = true;
        bool end = false;
        if (player.hp <= 0)
        {
            end = true;
            Debug.LogWarning("Player lost, End of encounter");
            win = false;
        }
        if (enemy.hp <= 0)
        {
            end = true;
            Debug.LogWarning("Enemy lost, End of encounter");
            win = true;
        }
        if(end)
            EndEncounter(win);
    }

    public void EndEncounter(bool win)
    { 
        StartCoroutine(LoadLevel(win));
    }
    Item DropRandomItem()
    {
        int equipableRoll = Random.Range(1, 4);

        int number = Random.Range(0, HardcodedItems.items.Keys.Count - 1);
        List<string> lista = Enumerable.ToList(HardcodedItems.items.Keys);
        if(lista[number] != "hpotion" && equipableRoll <=2)
        {
            return DropRandomItem();
        }

        if (HardcodedItems.items[lista[number]] is EquipableItem)
            return new EquipableItem((EquipableItem)HardcodedItems.items[lista[number]]);
        else
            return new UsableItem((UsableItem)HardcodedItems.items[lista[number]]);
    }
    private IEnumerator LoadLevel(bool win)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);

        while (!asyncLoadLevel.isDone)
        {
            print("Loading the Scene");
            yield return null;
        }
        //Vector3 pos = new SerializableVector3(PlayerPrefs.GetString("playerPos"));
        //GameObject.FindGameObjectWithTag("Player").transform.position = pos;
        //Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);

        string serializedPlayer = SaveManager.LoadFromCurrentSlot();

        string[] tab1 = serializedPlayer.Split('\n');
        string[] tab2 = tab1[0].Split(';');
        tab2[0] = player.hp.ToString();
        tab2[1] = player.hpMax.ToString();
        string s1 = string.Join(";", tab2);
        tab1[0] = s1;
        serializedPlayer = string.Join("\n", tab1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponent>().Deserialize(serializedPlayer);
        if(win)
        {
            Item droppedItem = DropRandomItem();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponent>().AddItem(droppedItem);
            UIManager.uIManager.ShowImportantMessage($"Result: {player} , {enemy}. Dropped: {droppedItem.name}");

        }
        else
        {
            UIManager.uIManager.ShowImportantMessage($"You lost.",() => SceneManager.LoadScene("MainMenu"));
        }
        
    }
}