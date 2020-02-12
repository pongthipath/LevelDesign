using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBehavior : MonoBehaviour
{
    public enum SkillId
    {
        noskill, skill_1, skill_2, skill_3, skill_4, skill_5, skill_6, skill_7, skill_8, skill_9, skill_10
    }

    public delegate void OrbsContainer(string skillName);
    public delegate void OrbDamage(float damage);
    public static event OrbsContainer EventOrbsContainer;
    public static event OrbDamage EventOrbDamage;

    public int[] Orbs = new int[3];
    Queue<int> OrbQueue = new Queue<int>();
    public string[] SpellOrder = new string[2];
    public string spellName = null;

    public SkillId spellNameId = SkillId.noskill;
    public static SkillId[] spellIdOrder = new SkillId[2];

    int i = 0;
    float damage = 0;
    private void Start()
    {
        spellIdOrder[0] = SkillId.noskill;
        spellIdOrder[1] = SkillId.noskill;
    }
    void Update()
    {
        if (OrbQueue.Count <= 2)
        {
            if (Input.GetButtonDown("Orb1"))
            {
                OrbQueue.Enqueue(1);
                Debug.Log("button1");
            }
            else if (Input.GetButtonDown("Orb2"))
            {
                OrbQueue.Enqueue(2);
                Debug.Log("button2");
            }
            else if (Input.GetButtonDown("Orb3"))
            {
                OrbQueue.Enqueue(3);
                Debug.Log("button3");
            }
        }
        else
        {
            // If queue has 3 elements it try to acquire the skill 
            // sai pummm sang skill otherwise, if more input clear it out

            int[] Orbs = OrbQueue.ToArray();
            for (int i = 0; i < Orbs.Length; i++)
            {
                Debug.Log("Orb index " + i + " = " + Orbs[i]);
            }
            SearchSpell(Orbs[0], Orbs[1], Orbs[2]);
        }

        if (Input.GetButtonDown("SkillCall"))
        {
            UsingSpell();

        }
    }
    void SearchSpell(int orb0, int orb1, int orb2)
    {
        if (orb0 == 1 && orb1 == 1 && orb2 == 1)
        {
            spellName = "Using spell 1";
            Debug.Log("Spell 1" + " White ball");

            // Try change string to enum id
            // From outside we have to use SkillBehaviorDelegate.SkillId.skill_1;
            spellIdOrder[0] = SkillId.skill_1;
        }
        if (orb0 == 2 && orb1 == 2 && orb2 == 2)
        {
            spellName = "Using spell 2";
            Debug.Log("Spell 2" + " Red ball");

            spellIdOrder[0] = SkillId.skill_2;
        }
        else if (orb0 == 3 && orb1 == 3 && orb2 == 3)
        {
            spellName = "Using spell 3";
            Debug.Log("Spell 3");

            spellIdOrder[0] = SkillId.skill_3;
        }
        else if (orb0 == 1 && orb1 == 1 && orb2 == 2)
        {
            spellName = "Using spell 4";
            Debug.Log("Spell 4");

            spellIdOrder[0] = SkillId.skill_4;
        }
        else if (orb0 == 1 && orb1 == 1 && orb2 == 3)
        {
            spellName = "Using spell 5";
            Debug.Log("Spell 5");

            spellIdOrder[0] = SkillId.skill_5;
        }
        else if (orb0 == 2 && orb1 == 2 && orb2 == 1)
        {
            spellName = "Using spell 6";
            Debug.Log("Spell 6");

            spellIdOrder[0] = SkillId.skill_6;
        }
        else if (orb0 == 2 && orb1 == 2 && orb2 == 3)
        {
            spellName = "Using spell 7";
            Debug.Log("Spell 7");

            spellIdOrder[0] = SkillId.skill_7;
        }
        else if (orb0 == 3 && orb1 == 3 && orb2 == 1)
        {
            spellName = "Using spell 8";
            Debug.Log("Spell 8");

            spellIdOrder[0] = SkillId.skill_8;
        }
        else if (orb0 == 3 && orb1 == 3 && orb2 == 2)
        {
            spellName = "Using spell 9";
            Debug.Log("Spell 9");

            spellIdOrder[0] = SkillId.skill_9;
        }
        else if (orb0 == 1 && orb1 == 2 && orb2 == 3)
        {
            spellName = "Using spell 10";
            Debug.Log("Spell 10");

            spellIdOrder[0] = SkillId.skill_10;
        }
        UsingSpell();
    }
    void UsingSpell()
    {
        Debug.Log("Using Skill ------!@#!@%!%");
        if (EventOrbDamage != null)
        {
            Debug.Log("EventOrbDamage  mai chai null !!!!!!!!");
            if (Input.GetButtonDown("SkillCall"))
            {
                Debug.Log("Fire Skill : Skill id = " + spellIdOrder[0] + ", damage = " + SpellNameToDamage(spellIdOrder[0]));
                if (spellIdOrder[0] == SkillId.noskill && spellIdOrder[1] != SkillId.noskill)
                {
                    damage = SpellNameToDamage(spellIdOrder[1]);
                }
                else
                {
                    damage = SpellNameToDamage(spellIdOrder[0]);
                }

                if (damage != 0)
                {
                    // Do damage from skill slot 0
                    EventOrbDamage(damage);
                }
                // Remember skill from slot 0 to 1
                spellIdOrder[1] = spellIdOrder[0]; 
                //EventOrbsContainer(spellIdOrder[0].ToString());
            }
            // clear combo on use skill button
            OrbQueue.Clear();
        }

        float SpellNameToDamage(SkillId spellName)
        {
            switch (spellName)
            {
                case SkillId.skill_1:
                    // Skill1 do 10 damage
                    return 10;
                case SkillId.skill_2:
                    return 20;
                case SkillId.skill_3:
                    return 30;
                case SkillId.skill_4:
                    return 40;
                case SkillId.skill_5:
                    return 50;
                case SkillId.skill_6:
                    return 60;
                case SkillId.skill_7:
                    return 70;
                case SkillId.skill_8:
                    return 80;
                case SkillId.skill_9:
                    return 90;
                case SkillId.skill_10:
                    return 100;
                default:
                    return 0;
            }
        }
    }
}
