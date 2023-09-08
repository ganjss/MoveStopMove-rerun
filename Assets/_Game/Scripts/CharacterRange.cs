using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRange : MonoBehaviour
{
    [SerializeField] Character character;
    private Enemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        // player found enemy or enemy found enemy
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // check if enemy exist
            if (enemy != null)
            {
                // cancel aim of old enemy
                enemy.aim.SetActive(false);
                enemy = null;
            }

            // get next enemy
            enemy = other.gameObject.GetComponent<Enemy>();
            character.FoundEnemy(other.transform);
        }

        // check if PlayerRange  found Enemy -> active aim. If Enemy found Enemy -> not active aim
        if (other.CompareTag("Enemy") && gameObject.CompareTag("PlayerRange"))
        {
            enemy.aim.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // player defound enemy or enemy defound enemy
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            character.DefoundEnemy();
        }

        // check if PlayerRange defound Enemy -> inactive aim
        if (other.CompareTag("Enemy") && gameObject.CompareTag("PlayerRange"))
        {
            enemy.aim.SetActive(false);
        }
    }

}
