using System.Collections;
using UnityEngine;

public class DragonStat : MonoBehaviour
{
    public float curHP = 200;
    public float maxHP = 2000;
    public float maxBAR = 100;
    public float HealthBarLength;
    public GameObject Gem;
    public GameObject Boss;
    public bool DestroyOnDeath = false;
    public int respawnTime = 5;
    DragonController RespawnPoint;
    //public Renderer Rend;

    // Start is called before the first frame update
    void Start()
    {
        SkillBehavior.EventOrbDamage += ChangeHP;
        Gem.transform.gameObject.SetActive(false);
        Boss.transform.gameObject.SetActive(true);
        RespawnPoint = gameObject.GetComponent<DragonController>();
        //Rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeHP(float Change)
    {
        Debug.Log(Change +" from delegate");
        curHP -= Change;
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
        if (curHP <= 0)
        {
            Die();
            Debug.Log("Boss were killed");
        }
    }
    void Die()
    {
        Boss.gameObject.SetActive(false);
        //Rend.enabled = false;
        Gem.transform.gameObject.SetActive(true);
        DestroyOnDeath = true;
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        if (DestroyOnDeath)
        {
            yield return new WaitForSeconds(respawnTime);
            Boss.gameObject.SetActive(true);
        }
        DestroyOnDeath = false;
        //Rend.enabled = true;
        RespawnPoint.MoveBack();
    }
}

