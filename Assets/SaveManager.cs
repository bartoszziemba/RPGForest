using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    public static SaveManager saveManager;
    static int slot = 1;
    public static bool inputEnabled = true;

    private void Awake()
    {
        if (saveManager == null)
            saveManager = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    public static void EnableInput()
    {
        inputEnabled = true;
    }

    public static void DisableInput()
    {
        inputEnabled = false;

    }
    public static void SaveOnCurrentSlot(string info)
    {
        Save(info);
    }
    public static void SaveOnSlot(string info,int slot)
    {
        int currentSlot = SaveManager.slot;
        SaveManager.slot = slot;
        SaveOnCurrentSlot(info);
        SaveManager.slot = currentSlot;
    }
    static void Save(string info)
    {
        System.IO.File.WriteAllText(GeneratePath(slot), info);
    }

    public static string LoadFromCurrentSlot()
    {
        return Load(SaveManager.slot);
    }

    public static string LoadFromSlot(int slot)
    {
        return Load(slot);
    }

    static string Load(int slot)
    {
        return System.IO.File.ReadAllText(GeneratePath(slot));
    }

    public void LoadGame(int slot)
    {
        SaveManager.slot = slot;
        StartCoroutine( LoadLevel(slot));
    }
    public static bool SlotIsEmpty(int slot)
    {
        return (new System.IO.FileInfo(GeneratePath(slot)).Length == 0);
    }
    private IEnumerator LoadLevel(int slot)
    {
        SaveManager.DisableInput();

        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Single);
        
        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading the Scene");
            yield return null;
        }
        string serializedPlayer;
        if (!SlotIsEmpty(slot))
        {
            serializedPlayer = SaveManager.LoadFromSlot(slot); 
        }
        else
        {
            serializedPlayer = RPGItems.HardcodedPlayers.defaultPlayer;
        }
        Debug.Log(serializedPlayer);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponent>().Deserialize(serializedPlayer);
        SaveManager.EnableInput();
    }
    public static string GetSlotInfo(int slot)
    {
        if(SlotIsEmpty(slot))
        {
            return $"Slot {slot} empty";
        }
        else
        {
            return $"Slot {slot} saved {System.IO.File.GetLastWriteTime(GeneratePath(slot))}";
        }
        
    }

    static string GeneratePath(int slot)
    {
        return $"SaveSlot{slot}.txt";
    }

    public void NewGame(int slot)
    {
        System.IO.File.WriteAllText(GeneratePath(slot), RPGItems.HardcodedPlayers.defaultPlayer);
        LoadGame(slot);
    }
    public static void InitializeSaveSystem()
    {
        for(int i=1;i<=3;i++)
        if(!System.IO.File.Exists(GeneratePath(i)))
        {
            System.IO.File.Create(GeneratePath(i));
        }
        System.IO.File.WriteAllText("DefaultPlayer.txt",RPGItems.HardcodedPlayers.defaultPlayer);
    }


}
