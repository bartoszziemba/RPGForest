using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterScript : MonoBehaviour
{
    public Text playerNameText;
    public Text enemyNameText;

    public Slider playerHpSlider;
    public Slider enemyHpSlider;

    public Button attackButton;

    public EncounterManger encounterManger;

    [SerializeField]
    int currentPlayerHealth;
    [SerializeField]
    int currentEnemyHealth;
    void Start()
    {
        encounterManger = GameObject.FindGameObjectWithTag("EncounterManager").GetComponent<EncounterManger>();
        attackButton.onClick.AddListener(OnAttackButton);
        playerNameText.text = encounterManger.player.name;
        enemyNameText.text = encounterManger.enemy.name;

        UpdateLocalHealthValuesToManager();

        RefreshHealthSliders();
    }
    void UpdateLocalHealthValuesToManager()
    {
        currentEnemyHealth = encounterManger.enemy.hp;
        currentPlayerHealth = encounterManger.player.hp;
    }
    void OnAttackButton()
    {
        attackButton.interactable = false;
        encounterManger.AttackEnemy();
        

        StartCoroutine(SmoothHealthChange());

        //RefreshHealthSliders();
    }
    
    void RefreshHealthSliders()
    {
        float playerValue = currentPlayerHealth / (float)encounterManger.player.hpMax;
        float enemyValue = currentEnemyHealth / (float)encounterManger.enemy.hpMax;

        float playerColor = 100 * playerValue / 360;
        float enemyColor = 100 * enemyValue / 360;

        playerHpSlider.value = playerValue;
        enemyHpSlider.value = enemyValue;

        playerHpSlider.targetGraphic.color = Color.HSVToRGB(playerColor,1,1);
        enemyHpSlider.targetGraphic.color = Color.HSVToRGB(enemyColor, 1, 1);
    }
    IEnumerator SmoothHealthChange()
    {
        while(currentEnemyHealth != encounterManger.enemy.hp)
        {
            
            currentEnemyHealth = (int)Mathf.Lerp(currentEnemyHealth, encounterManger.enemy.hp, 0.1f);
            RefreshHealthSliders();
            yield return new WaitForSeconds(0.05f);
        }

        while (currentPlayerHealth != encounterManger.player.hp)
        {
            currentPlayerHealth = (int)Mathf.Lerp(currentPlayerHealth, encounterManger.player.hp, 0.1f);
            RefreshHealthSliders();
            yield return new WaitForSeconds(0.05f);
        }
        encounterManger.CheckEndOfEncounter();
        attackButton.interactable = true;
        
    }

    void Update()
    {
        
    }
}
