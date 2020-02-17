using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro TextMesh;
    public Transform pfDamagePopup;
    Color textColor;
    Vector3 moveVector;
    float disappearTimer = 1f;
    float disappearSpeed = 3f;
    static int sortingOrder;
    public DamagePopup Create(Vector3 position, float damageAmount)
    {
        Transform DamagePopupTransform = Instantiate(pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = DamagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup;
    }
    private void Awake()
    {
        TextMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(float damageAmount)
    {
        TextMesh.SetText(damageAmount.ToString());
        textColor = TextMesh.color;
        moveVector = new Vector3(.7f, 1) * 60f;
        sortingOrder++;
        TextMesh.sortingOrder = sortingOrder; 
    }
    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            TextMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
