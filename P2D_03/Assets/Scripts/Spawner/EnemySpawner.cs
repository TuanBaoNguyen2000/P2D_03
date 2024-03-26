using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    public static EnemySpawner Instance;

    [Header("Prefab Name")]
    public static string Rat = "Rat";


    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Only 1 VFXSpawner allow to exist!");
        Instance = this;
    }

}
