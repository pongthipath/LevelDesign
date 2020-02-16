using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public UnitType unitType;

    public enum UnitType
    {
        RedPlayer, BluePlayer, Dragon, Environment
    }
}
