using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour, IDamageable
{

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _minHealth = 1;
    public int currentHealth;

    public int CurrentHealth { get; set ;}
    public int MaxHealth { get; set; }
    public int MinHealth { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = _maxHealth;
        MinHealth = _minHealth;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = CurrentHealth;
    }

    public void Damage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth < MinHealth)
        {
            CurrentHealth = 0;
            Destroy(this.gameObject);
        }
    }
}
