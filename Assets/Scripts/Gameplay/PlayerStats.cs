using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Range(1, 100)]
    public int customHealth = 20;
    public int currentHealth;
    public Slider healthSlider;
    [Range(1, 100)]
    public int customDamage = 5;
    public int currentDamage;
    public Slider damageSlider;
    [Range(1, 100)]
    public int customSpeed = 10;
    public int currentSpeed;
    public Slider speedSlider;
    [Range(1, 100)]
    public int customjumpForce = 15;
    public int currentjumpForce;
    public Slider jumpForceSlider;

    void Start()
    {
        currentHealth = customHealth;
        healthSlider.maxValue = customHealth;
        healthSlider.value = customHealth;
        currentDamage = customDamage;
        damageSlider.maxValue = customDamage;
        damageSlider.value = currentDamage;
        currentSpeed = customSpeed;
        speedSlider.maxValue = customSpeed;
        speedSlider.value = currentSpeed;
        currentjumpForce = customjumpForce;
        jumpForceSlider.maxValue = customjumpForce;
        jumpForceSlider.value = currentjumpForce;
    }

    void Update()
    {
        healthSlider.value = currentHealth;
        damageSlider.value = currentDamage;
        speedSlider.value = currentSpeed;
        jumpForceSlider.value = currentjumpForce;
    }
}