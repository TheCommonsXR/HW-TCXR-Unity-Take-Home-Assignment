using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject bar;

    void Start()
    {
        // Set the max HP for the health bar.
        if (bar != null)
        {
            bar.transform.localScale = new Vector3(1, bar.transform.localScale.y, bar.transform.localScale.z);
        }
    }

    public void UpdateHealthBar(float currentHP, float maxHP)
    {
        if (bar != null)
        {
            float healthPercentage = currentHP / maxHP;
            bar.transform.localScale = new Vector3(healthPercentage, bar.transform.localScale.y, bar.transform.localScale.z);
        }
    }

    public void SetHealthBarActive(bool active)
    {
        if (bar != null)
        {
            bar.transform.parent.gameObject.SetActive(active);
        }
    }
}
