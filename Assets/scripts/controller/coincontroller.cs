using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coincontroller : MonoBehaviour
{
   [Header("Coin Settings")]
    [SerializeField] private int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only collect coin if player touches it
        if (other.CompareTag("Player"))
        {
            // Load current coin count, add value, and save
            int currentCoins = PlayerPrefs.GetInt("Coins", 0);
            currentCoins += coinValue;
            PlayerPrefs.SetInt("Coins", currentCoins);
            PlayerPrefs.Save();

            // (Optional) Log for testing
            Debug.Log("Coins collected: " + currentCoins);

            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
