using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//question 4
[CreateAssetMenu(fileName = "NewGameMode", menuName = "Game/Game Mode")]
public class gamemode : ScriptableObject
{
    public int hp;
    public Vector3 playerStartPosition;
}
