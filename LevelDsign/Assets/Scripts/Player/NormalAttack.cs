using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public float AttkForce = 100f;
    public float AttDamage = 50f;
    public Camera CamFPS;
    public Camera CamTPS;
    private void Start()
    {
        
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
            if(Physics.Raycast(CamTPS.transform.position, CamTPS.transform.forward, out hitTPS, AttkForce))
            {
                Debug.Log(hitTPS.transform.name + " TPS");

                DragonStat boss = hitTPS.transform.GetComponent<DragonStat>();
                if (boss != null)
                {
                    boss.ChangeHP(AttDamage);
                }
                //GameObject attack = Instantiate(bullet, hand.transform.position, hand.transform.rotation) as GameObject;
                //Rigidbody rigidbody = attack.GetComponent<Rigidbody>();
                //rigidbody.AddForce(hand.transform.forward * AttkForce, ForceMode.Impulse);
                //Destroy(attack, 1f);
            }
        }
        if (PlayerController.camMode == 1)
        {
            if (Physics.Raycast(CamFPS.transform.position, CamFPS.transform.forward, out hitFPS, AttkForce))
            {
                Debug.Log(hitFPS.transform.name + " FPS");
                DragonStat boss = hitFPS.transform.GetComponent<DragonStat>();
                if (boss != null)
                {
                    boss.ChangeHP(AttDamage);
                }
            }
        }
        
    }
}
