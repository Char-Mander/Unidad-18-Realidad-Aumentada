using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTracking : DefaultTrackableEventHandler
{
    public bool isTracked;
    bool cameramode = false;
    Monster myMonster;

    protected override void Start()
    {
        base.Start();
        myMonster = GetComponent<Monster>();
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        PlayerIsTracked();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        PlayerLost();
    }

    public void PlayerIsTracked()
    {
        print(gameObject.name + " encontrado");
        isTracked = true;
        CombatManager.instance.RegistPlayer(myMonster);
        CombatManager.instance.SetPlayerTracked(myMonster, true);
    }

    public void PlayerLost()
    {
       // print(gameObject.name + " perdido");
        isTracked = false;
        CombatManager.instance.SetPlayerTracked(myMonster, false);
    }
    
}
