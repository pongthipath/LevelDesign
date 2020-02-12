using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    // -------- Health system -------
    public float maxHP;
    public float curHP;
    public bool isHPBelowZero = true;

    public delegate void HealthChangeDelegate();

    public HealthChangeDelegate modifyHealth;
    public Slider HPBar;


    // -------- Stamina -------------
    public float maxSP;
    private float SP_decreaseRate;
    public float SP_decrease;
    private float SP_increaseRate;
    public float SP_increase;
    public float curSP;
    public Slider StaminaBarLength;

    private PlayerController playerController;
    private Rigidbody rb;
    private Animator stat_anim;

    private void Awake()
    {
        //----------------- Stamina -------------------
        //StaminaBarLength.maxValue = maxSP;
        StaminaBarLength.value = maxSP;

        SP_decreaseRate = 5;
        SP_increaseRate = 5;

        playerController = GetComponent<PlayerController>();
        stat_anim = playerController.GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // ----------------- Health System --------------
        maxHP = 100;
        HPBar.maxValue = maxHP;
        HPBar.value = maxHP;
        IsHPBelowZero();
        //----------------- Stamina -------------------
        StartCoroutine(StaminaStats());
    }
    // Update is called once per frame
    private void Update()
    {

    }
    //------------- health system -------------
    public void HP_modify(float amount)
    {
        if (curHP + amount > maxHP && maxHP > 0)
        {
            amount = maxHP - curHP;
        }
        curHP += amount;
        HPBar.value = curHP;
        IsHPBelowZero();

        modifyHealth();
    }
    public bool IsHPBelowZero()
    {
        if (maxHP <= 0)
            maxHP = 1.0f;

        //dead
        if (curHP <= 0)
        {
            isHPBelowZero = true;
        }
        else
        {
            isHPBelowZero = false;
        }
        return isHPBelowZero;
    }

    //--------------- Stamina function using coroutine ---------------
    IEnumerator StaminaStats()
    {

        while (true)
        {
            if (rb.velocity.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                StaminaBarLength.value -= Time.deltaTime / SP_decreaseRate * SP_decrease;
                curSP = StaminaBarLength.value;
            }
            else
            {

                StaminaBarLength.value += Time.deltaTime / SP_increaseRate * SP_increase;
                curSP = StaminaBarLength.value;
                yield return new WaitForSeconds(2f);

            }
            if (StaminaBarLength.value >= maxSP)
            {
                StaminaBarLength.value = maxSP;
            }
            else if (StaminaBarLength.value <= 0)
            {
                //StaminaBarLength.value = 0;
                playerController.runSpeed = playerController.walkSpeed;

                stat_anim.SetBool("isWalking", true);
                stat_anim.SetBool("isRunning", false);
                //stat_anim.SetBool("isIdle", false);

            }
            else if (StaminaBarLength.value >= 0)
            {
                playerController.runSpeed = playerController.runSpeedNorm;

                //stat_anim.SetBool("isRunning", true);
                //stat_anim.SetBool("isWalking", false);
                //stat_anim.SetBool("isIdle", false);
            }
            yield return null;
        }
    }
}
        

