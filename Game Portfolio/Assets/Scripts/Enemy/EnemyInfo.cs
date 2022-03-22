using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [Header("Main stats")]
    [Tooltip("HP amount of an enemy")]
    public int hp;



    [HideInInspector] public int currentHp;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        currentHp = hp;

    }
}
