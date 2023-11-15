using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, WAIT }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Character playerUnit;
    Enemy enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public EnemyHUD enemyHUD;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Character>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Enemy>();

        dialogueText.text = "A " + enemyUnit.enemyName + " is attacking!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(1f);

        dialogueText.text = playerUnit.unitName + "'s turn!";

    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        int hasEnergy = playerUnit.Attack(playerUnit.costOfAttack);

        
        if (hasEnergy >= 0)
        {
            enemyHUD.SetHP(enemyUnit.currentHP);
            playerHUD.SetEP(playerUnit.currentEP);

            dialogueText.text = "Attack deals " + playerUnit.damage + " damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();

            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());

            }
        }
        else
        {
            dialogueText.text = "Your energy is too low!";
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }


        
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        dialogueText.text = enemyUnit.enemyName + " deals " + enemyUnit.damage + " damage!";

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();

        }
        else
        {
            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());

        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Victory!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You have died...";
        }

    }

    public void onAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        state = BattleState.WAIT;
        StartCoroutine(PlayerAttack());
    }

    /*public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Character playerUnit;
    Enemy enemyUnit;

    public Text characterInfo;
    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Character>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Enemy>();

        characterInfo.text = playerUnit.unitName;

        playerHUD.SetHUDCharacter(playerUnit);
        enemyHUD.SetHUDEnemy(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHPEnemy(enemyUnit.currentHP);
        dialogueText.text = "The attack is successful!";

        // Damage the enemy

        state = BattleState.WAIT;

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }


        // Change state based on what happened

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }

    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.enemyName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHPCharacter(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }



    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHPCharacter(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        state = BattleState.WAIT;

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }


    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }*/

}