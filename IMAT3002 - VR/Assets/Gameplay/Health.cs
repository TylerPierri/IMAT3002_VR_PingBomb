
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    public UnityAction deathAction;

    private void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = healthSlider.value.ToString("F0");
    }

    public void takeDamage(float damage)
    {
        if ((currentHealth - damage) >= 0)
            currentHealth -= damage;
        else
        {
            currentHealth = 0;

            if(deathAction != null)
                deathAction.Invoke();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text = healthSlider.value.ToString("F0");
    }
}
