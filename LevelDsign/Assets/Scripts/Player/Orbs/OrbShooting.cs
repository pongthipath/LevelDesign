using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbShooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        SkillBehavior.EventOrbShoot += OrbEffect;
    }
    void OrbEffect(string SkillName)
    {
        Debug.Log(SkillName + " from orb");
        if (SkillName == "skill_1")
        {
            Debug.Log(SkillName + " pressed");
            GameObject attack = Instantiate(bullet, hand.transform.position, hand.transform.rotation) as GameObject;
            Rigidbody rigidbody = attack.GetComponent<Rigidbody>();
            rigidbody.AddForce(hand.transform.forward * 500f, ForceMode.Impulse);
        }
    }
}
