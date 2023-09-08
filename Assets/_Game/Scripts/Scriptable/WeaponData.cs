using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponBulletMapping
{
    public WeaponType weaponType;
    public BulletType bulletType;
}

public enum WeaponType
{
    None = 0,
    Hammer = 1,
    Axe0 = 2,
    Axe1 = 3,
    Bummerang = 4
}

[CreateAssetMenu(menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] Weapon[] weapons;
    [SerializeField] private WeaponBulletMapping[] weaponBulletMappings;

    public Weapon GetWeapon(WeaponType weaponType)
    {
        return weapons[(int)weaponType];
    }

    public BulletType GetBulletType(WeaponType weaponType)
    {
        for (int i = 0; i < weaponBulletMappings.Length; i++)
        {
            if (weaponBulletMappings[i].weaponType == weaponType)
            {
                return weaponBulletMappings[i].bulletType;
            }
        }
        return BulletType.None;
    }
}
