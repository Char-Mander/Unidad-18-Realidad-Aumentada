using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState {  WAITING, SINCRO, TURN_P1, TURN_P2, END}

public class CombatManager : MonoBehaviour
{

    #region Singleton
    public static CombatManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public class Player
    {
        public Monster playerMonster;
        public bool isTracked;

        public Player(Monster pMonster)
        {
            playerMonster = pMonster;
            isTracked = true;
        }
    }

    public float timeBetweenTurns = 3;
    public CombatState state;

    Player player1 = null;
    Player player2 = null;

    bool player1Turn = true;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatState.WAITING;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CombatState.WAITING:
                if(player1.isTracked && player2.isTracked)
                {
                    state = CombatState.SINCRO;
                    print("Both players are registered");
                }
                break;
            case CombatState.SINCRO: 
                if(player1.isTracked && player2.isTracked)
                {
                    print("Both players are ready");
                    if (player1Turn)
                    {
                        state = CombatState.TURN_P1;
                        StartCoroutine(StartP1Turn());
                    }
                    else
                    {
                        state = CombatState.TURN_P2;
                        StartCoroutine(StartP2Turn());
                    }
                }
                break;
        }
    }

    public void RegistPlayer(Monster monster)
    {
        if(player1 == null)
        {
            player1 = new Player(monster);
            Debug.Log(player1.playerMonster.mName + " registered P1");
        }
        else if(player2 == null && player1.playerMonster != monster)
        {
            player2 = new Player(monster);
            Debug.Log(player2.playerMonster.mName + " registered P2");
        }
    }

    public void SetPlayerTracked(Monster monster, bool isTracked)
    {
        if(player1.playerMonster == monster)
        {
            player1.isTracked = isTracked;
        }
        else if(player2.playerMonster == monster)
        {
            player2.isTracked = isTracked;
        }
        else { Debug.Log("Error"); }
    }

    IEnumerator StartP1Turn()
    {
        yield return new WaitForSeconds(timeBetweenTurns);
        player1Turn = false;
        state = CombatState.SINCRO;
    }

    IEnumerator StartP2Turn()
    {
        yield return new WaitForSeconds(timeBetweenTurns);
        player1Turn = true;
        state = CombatState.SINCRO;
    }

}
