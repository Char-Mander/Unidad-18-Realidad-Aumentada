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
        public MonsterCanvasController mCanvas;
        public Animator anim;

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

    public void RegistPlayer(Monster monster, MonsterCanvasController mCanvas, Animator _anim)
    {
        if(player1 == null)
        {
            player1 = new Player(monster);
            player1.mCanvas = mCanvas;
            player1.anim = _anim;
            Debug.Log(player1.playerMonster.mName + " registered P1");
        }
        else if(player2 == null && player1.playerMonster != monster)
        {
            player2 = new Player(monster);
            player2.mCanvas = mCanvas;
            player2.anim = _anim;
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
        print("Turno player1");
        player1.playerMonster.Attack(player2.playerMonster);
        player1.anim.SetTrigger("Attack 02");
        player2.mCanvas.UpdateMonsterHp(player2.playerMonster.currentHp, player2.playerMonster.maxHp);
        yield return new WaitForSeconds(timeBetweenTurns);
        player1Turn = false;
        if (player2.playerMonster.currentHp > 0)
            state = CombatState.SINCRO;
        else
        {
            state = CombatState.END;
            player2.anim.SetTrigger("Die");
            StartCoroutine(EndFight());
        }
    }

    IEnumerator StartP2Turn()
    {
        print("Turno player2");
        player2.playerMonster.Attack(player1.playerMonster);
        player2.anim.SetTrigger("Attack 02");
        player1.mCanvas.UpdateMonsterHp(player1.playerMonster.currentHp, player1.playerMonster.maxHp);
        yield return new WaitForSeconds(timeBetweenTurns);
        player1Turn = true;
        if (player1.playerMonster.currentHp > 0)
            state = CombatState.SINCRO;
        else
        {
            state = CombatState.END;
            player1.anim.SetTrigger("Die");
            StartCoroutine(EndFight());
        }
    }


    IEnumerator EndFight()
    {
        yield return new WaitForSeconds(timeBetweenTurns);
        player1 = null;
        player2 = null;
        MonsterCreator[] mcs = FindObjectsOfType<MonsterCreator>();
        foreach(MonsterCreator mc in mcs){
            mc.monster = null;
            Destroy(mc.transform.GetChild(0).transform.GetChild(0).gameObject);
        }
        yield return new WaitForSeconds(timeBetweenTurns);
        state = CombatState.WAITING;
    }
}
