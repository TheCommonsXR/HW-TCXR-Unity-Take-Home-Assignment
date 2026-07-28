using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    public TMP_Text damageNumber;

    /// <summary>
    /// Setup text and color values
    /// </summary>
    public void Setup(int damage, Color color)
    {
        damageNumber.text = damage.ToString();
        damageNumber.color = color;
    }

    /// <summary>
    /// Destroy at end of anim
    /// </summary>
    void Destruct()
    {
        Destroy(gameObject);
    }
}
