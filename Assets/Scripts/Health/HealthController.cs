using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializedField]
    private float _currentHealth;

    [SerializedField] //set to inspector
    private float _maximumHealth;

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public void TakeDamage (float damageAmount)
    {
        if (_currentHealth == 0)
            return;

        _currentHealth -= damageAmount;

        if(_currentHealth < 0)
            _currentHealth = 0;
    }

    public void AddHealth (float amountToAdd)
    {
        if (_currentHealth == _maximumHealth)
            return;

        _currentHealth += amountToAdd;

        if (_currentHealth > _maximumHealth)
        {
                _currentHealth = _maximumHealth;
        }
    }
}
