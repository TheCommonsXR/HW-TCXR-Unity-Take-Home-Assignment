using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Start is called before the first frame update
    [CreateAssetMenu(fileName = "New Game Mode", menuName = "Game/Game Mode")]
    public class GameModeConfig : ScriptableObject
    {
    [Header("Player Settings")]
    public int playerMaxHealth = 5;

    public Vector3 playerStartPosition = Vector3.zero;

    public float playerSpeed = 7f;
    }   
