using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject[] bullets;

    public GameObject GetWeapon(WeaponType weaponType)
    {
        return weapons[(int)weaponType];
    }

    public GameObject GetBullet(WeaponType weaponType)
    {
        return bullets[(int)weaponType];
    }

}
