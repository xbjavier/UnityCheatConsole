using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCheatConsole;

public class Character : MonoBehaviour
{
    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

    private int currentExperience = 0;

    private void OnEnable()
    {
        CheatController.AddCheatsFromBehaviour(this);
    }

    private void OnDisable()
    {
        CheatController.RemoveCheatBehaviour(this);
    }
    private void Awake()
    {
        currentHP = maxHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [CheatCode("damage", "deals damage to character", "damage.{instance}#intAmount")]
    public void Damage(int amount)
    {
        currentHP -= amount;
    }

    [CheatCode("add_exp", "add experience to character", "add_exp.{instance}#intAmount")]
    public void GainExperience(int amount)
    {
        currentExperience += amount;
    }

}
