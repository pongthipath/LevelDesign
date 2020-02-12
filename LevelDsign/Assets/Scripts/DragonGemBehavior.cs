﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonGemBehavior : MonoBehaviour
{
    public int RgemCount = 0;
    public int BgemCount = 0;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "RedPlayer")
        {
            RgemCount += 50;
            gameObject.SetActive(false);
            //StartCoroutine(DisableObjectAfterSeconds(3.0f));
            Debug.Log("Red team gets 50 gems");
        }
        if (collider.tag == "BluePlayer")
        {
            BgemCount += 50;
            gameObject.SetActive(false);
            //StartCoroutine(DisableObjectAfterSeconds(3.0f));
            Debug.Log("Blue team gets 50 gems");
        }
       
        
    }

    //IEnumerator DisableObjectAfterSeconds(float waitTime)
    //{
    //    GetComponent<MeshRenderer>().enabled = false;
    //    GetComponent<SphereCollider>().enabled = false;
    //    yield return new WaitForSeconds(waitTime);
    //    gameObject.SetActive(false);

    //}
}
