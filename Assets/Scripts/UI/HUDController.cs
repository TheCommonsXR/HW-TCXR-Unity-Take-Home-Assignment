using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI playerHealthTMP;

    public void UpdateHealthTMP(int currHealth)
    {
        playerHealthTMP.text = "Health - " + currHealth;
    }
}
