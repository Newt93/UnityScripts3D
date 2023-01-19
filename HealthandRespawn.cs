using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float maxShield = 50f;
    public float currentShield;
    public float shieldRegenSpeed = 10f;
    public float shieldRegenDelay = 5f;
    public float damageInvincibilityDuration = 1f;
    public bool isInvincible = false;

    private float shieldRegenTimer = 0f;
    private float damageInvincibilityTimer = 0f;

    private bool isDead = false;
    private bool shieldRegenActive = true;

    private void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    private void Update()
    {
        if (currentShield < maxShield && shieldRegenActive)
        {
            shieldRegenTimer += Time.deltaTime;
            if (shieldRegenTimer >= shieldRegenDelay)
            {
                currentShield += shieldRegenSpeed * Time.deltaTime;
                currentShield = Mathf.Clamp(currentShield, 0f, maxShield);
            }
        }

        if (isInvincible)
        {
            damageInvincibilityTimer += Time.deltaTime;
            if (damageInvincibilityTimer >= damageInvincibilityDuration)
            {
                isInvincible = false;
                damageInvincibilityTimer = 0f;
            }
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            if (currentShield > 0)
            {
                currentShield -= damage;
                shieldRegenTimer = 0f;
                shieldRegenActive = false;
            }
            else
            {
                currentHealth -= damage;
                isInvincible = true;
            }

            if (currentHealth <= 0 && !isDead)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        // Play death animation
        GetComponent<Animator>().SetTrigger("Die");
        // Disable player controls
        GetComponent<PlayerMovement>().enabled = false;
        // Find the nearest checkpoint and respawn the player
        Checkpoint nearestCheckpoint = FindNearestCheckpoint();
        transform.position = nearestCheckpoint.transform.position;
        // Reset player's health and shield
        currentHealth = maxHealth;
        currentShield = maxShield;
        // Re-enable player controls
        GetComponent<PlayerMovement>().enabled = true;
        isDead = false;
    }

    Checkpoint FindNearestCheckpoint()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        Checkpoint nearestCheckpoint = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            float distance = Vector3.Distance(transform.position, checkpoint.transform.position);
            if (distance < nearestDistance)
            {
                nearestCheckpoint = checkpoint;
                nearestDistance = distance;
            }
        }

        return nearestCheckpoint;
    }

