using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState {
        placing,
        picking,
        defending,
        defeat
    }

    public GameState gameState = GameState.placing;
    public int gold = 0;
    public int placedStones = 0;
}
