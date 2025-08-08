using UnityEngine;
using TMPro; // ✅ Required for TextMeshPro

public class CoinUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text coinText;

    void Update()
    {
        int currentCoins = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = "Coins: " + currentCoins;
    }
}
