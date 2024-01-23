using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitData
{
    public static float damageBonus = 0f;
    public static float armorBonus = 0f;

    public static float spearmanBonusDamage = 5f;
    public static float archerBonusDamage = 4f;
    public static float horsemanBonusDamage = 4f;
}
public class Unit
{
    protected string name;
    protected float health;
    protected float damage;
    protected float armor;

    public string Name { get { return name; } }
    public float Health { get; }
    public float Damage { get; }
    public float Armor { get; }

    public Unit() {}

    public Unit(string _name, float _health, float _damage, float _armor)
    {
        name = _name;
        health = _health;
        damage = _damage;
        armor = _armor;
    }
    public Unit(Unit unit)
    {
        name = unit.name;
        health = unit.health;
        damage = unit.damage;
        armor = unit.armor;
    }
    public void UpgradeStats(string stat, float amount)
    {
        switch(stat)
        {
            case "health": health += amount; break;
            case "damage": damage += amount; break;
            case "armor": armor += amount; break;
        }
    }
    public void TakeDamage(string enemy, float _damage)
    {
        switch(enemy)
        {
            case "spearman":
                if (name == "horseman") health -= (UnitData.spearmanBonusDamage + _damage);
                else health -= _damage;
                break;
            case "archer":
                if (name == "spearman") health -= (UnitData.archerBonusDamage + _damage);
                else health -= _damage;
                break;
            case "horseman":
                if (name == "archer") health -= (UnitData.horsemanBonusDamage + _damage);
                else health -= _damage;
                break;
        }

        if (health <= 0f) RemoveUnit();
    }

    public void RemoveUnit()
    {

    }
}
public class Spearman:Unit
{
    public Spearman()
    {
        name = "spearman";
        health = 10f;
        damage = 2f + UnitData.damageBonus;
        armor = 0f + UnitData.armorBonus;
    }
    public Spearman(string _name, float _health, float _damage, float _armor) : base(_name, _health, _damage, _armor) {}
}
public class Archer:Unit
{
    public Archer()
    {
        name = "archer";
        health = 8f;
        damage = 2f + UnitData.damageBonus;
        armor = 0f + UnitData.armorBonus;
    }
    public Archer(string _name, float _health, float _damage, float _armor) : base(_name, _health, _damage, _armor) {}
}
public class Horseman:Unit
{
    public Horseman()
    {
        name = "horseman";
        health = 12f;
        damage = 2f + UnitData.damageBonus;
        armor = 0f + UnitData.armorBonus;
    }
    public Horseman(string _name, float _health, float _damage, float _armor) : base(_name, _health, _damage, _armor) {}
}
