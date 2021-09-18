using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    int CurrentHealth { get; set; }
    int MaxHealth { get; set; }
    int MinHealth { get; set; }
    void Damage(int damage);

}
