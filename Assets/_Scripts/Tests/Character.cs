using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int maxHP = 100;
    [SerializeField] int currentHP;

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

    [CheatCode("damage", "deals damage to character")]
    public void Damage(int amount)
    {
        currentHP -= amount;
    }
}
