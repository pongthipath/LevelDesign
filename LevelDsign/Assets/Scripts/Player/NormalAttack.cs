using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public float AttkRange = 50f;
    public float AttDamage = 50f;
    public Camera CamFPS;
    public Camera CamTPS;
    DamagePopup popupDamage;
    DragonController getPosition;

    private void Start()
    {
        popupDamage = GetComponent<DamagePopup>();
        getPosition = GetComponent<DragonController>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    public void Attack()
    {
        RaycastHit hitTPS, hitFPS;
        if (PlayerController.camMode == 0)
        {
            if(Physics.Raycast(CamTPS.transform.position, CamTPS.transform.forward, out hitTPS, AttkRange))
            {
                Debug.Log(hitTPS.transform.name + " TPS");

                DragonStat bossTPS = hitTPS.transform.GetComponent<DragonStat>();
                if (bossTPS != null)
                {
                    bossTPS.ChangeHP(AttDamage);
                    //popupDamage.Create(getPosition.DragonPos, AttDamage);
                }
            }
        }
        if (PlayerController.camMode == 1)
        {
            if (Physics.Raycast(CamFPS.transform.position, CamFPS.transform.forward, out hitFPS, AttkRange))
            {
                Debug.Log(hitFPS.transform.name + " FPS");
                DragonStat bossFPS = hitFPS.transform.GetComponent<DragonStat>();
                if (bossFPS != null)
                {
                    bossFPS.ChangeHP(AttDamage);
                    //popupDamage.Create(getPosition.DragonPos, AttDamage);
                }
            }
        }
        
    }
}
