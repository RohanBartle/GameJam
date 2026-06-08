using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int startHealth = 100;

    [Header("UI Display")]
    [SerializeField] private TextMeshProUGUI healthText;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    private bool isDead = false;

    private void Awake()
    {
        startHealth = Mathf.Clamp(startHealth, 1, maxHealth);
        CurrentHealth = startHealth;
        UpdateHealthUI();
    }

    public void AddHealth(int amount)
    {
        if (isDead || amount == 0)
            return;

        int old = CurrentHealth;
        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);

        if (CurrentHealth != old)
            UpdateHealthUI();

        if (CurrentHealth <= 0 && !isDead)
            DieAndRestart();
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        AddHealth(-amount);
    }

    private void DieAndRestart()
    {
        isDead = true;

        StartCoroutine(RestartAfterDelay(1f));
        Scene active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.buildIndex, LoadSceneMode.Single);
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = $"Health: {CurrentHealth}/{maxHealth}";
    }

    private IEnumerator RestartAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}