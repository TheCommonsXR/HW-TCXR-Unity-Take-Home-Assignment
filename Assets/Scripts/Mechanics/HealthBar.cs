using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject bar;
    [SerializeField]
    private Animator barAnimator;

    void Awake()
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
        if (active)
        {
            barAnimator.Play("HealthBarAppear", 0, 0f);
        }
        else
        {
            barAnimator.Play("HealthBarDisappear", 0, 0f);
        }
    }
}
