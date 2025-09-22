using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarRenderer : MonoBehaviour {
    [SerializeField] private Slider slider;

    private void OnEnable() {
        GameDelegates.OnPlayerHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(int newHealth, int newMaxHealth) {
        if (newMaxHealth == 0) {
            throw new ArgumentOutOfRangeException(nameof(newMaxHealth), "Max health must be greater than zero.");
        }
        slider.value = (float)newHealth / newMaxHealth;
    }
}
