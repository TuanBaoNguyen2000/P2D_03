using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpawner : Spawner
{
    public static VFXSpawner Instance;

    [Header("Prefab Name")]
    public static string FlashVFX = "FlashVFX";
    public static string SlashVFX = "SlashVFX";
    public static string JabVFX = "JabVFX";
    public static string BackSlashVFX = "BackSlashVFX";


    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Only 1 VFXSpawner allow to exist!");
        Instance = this;
    }

}
