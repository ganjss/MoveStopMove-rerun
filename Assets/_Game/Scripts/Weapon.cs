using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // set state for weapon
    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
