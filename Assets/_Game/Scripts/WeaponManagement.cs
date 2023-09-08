using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;

    public void SetStateWeapon(bool isState)
    {
        gameObject.SetActive(isState);
    }
}
