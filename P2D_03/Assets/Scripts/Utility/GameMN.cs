using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMN : MonoBehaviour
{
    [Header("Layer Manager")]
    [SerializeField] private int playerLayer;
    [SerializeField] private int enemyLayer;

    void Start()
    {
        Physics.IgnoreLayerCollision(enemyLayer, enemyLayer);
        Physics.IgnoreLayerCollision(playerLayer, enemyLayer);
    }

}
