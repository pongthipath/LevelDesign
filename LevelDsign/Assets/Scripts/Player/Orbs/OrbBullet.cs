using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBullet : MonoBehaviour
{
    public delegate void OrbDamage(float damage);
    public static event OrbDamage EventOrbDamage;

    void OnTriggerEnter(Collider collider)
    {
        Unit unitType = collider.GetComponent<Unit>();
        if (unitType != null)
        {
            if (EventOrbDamage != null)
            {
                switch (unitType.unitType)
                {
                    case Unit.UnitType.Dragon:
                        Debug.Log("Dragon were hit");
                        Destroy(gameObject);
                        EventOrbDamage(50);
                        break;
                    case Unit.UnitType.Environment:
                        break;
                    case Unit.UnitType.BluePlayer:
                        break;
                    case Unit.UnitType.RedPlayer:
                        break;
                }
            }
        }
    }
}
