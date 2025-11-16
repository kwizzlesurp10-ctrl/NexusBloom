using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ForageGameManager : MonoBehaviour {
    [SerializeField] private int xpPerFind = 10;
    private enum GameState { Scanning, Identifying, Harvesting }
    private GameState currentState = GameState.Scanning;
    private int playerXP = 0;

    public void OnPlantScanned(ARTrackedImage trackedImage) {
        if (currentState == GameState.Scanning) {
            currentState = GameState.Identifying;
            playerXP += xpPerFind;
            Debug.Log($"XP Gained: {xpPerFind} | Total: {playerXP} | State: {currentState}");
        }
    }

    public void AdvanceState() { currentState = (GameState)(((int)currentState + 1) % 3); }
}