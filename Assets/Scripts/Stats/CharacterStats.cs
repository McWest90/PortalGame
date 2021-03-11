using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public bool isAlive = true;

    public Stat defaultHealth;
    public int currentHealth { get; private set; } 

    public Stat armor;

    public enum CharacterTypes
    {
        Player,
        Enemy
    }

    public CharacterTypes characterType;

    EnemyController controller;
    
    void Start() 
    {
        if(characterType == CharacterTypes.Enemy)
        {
            controller = GetComponent<EnemyController>();
        }
        
    }
    void Awake() 
    {
        currentHealth = defaultHealth.getValue();
        
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth > 0){
            damage -= armor.getValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);

            currentHealth -= damage;
        }

        if(currentHealth <= 0)
        {
            isAlive = false;
            controller.Die();
        }
    }
}
