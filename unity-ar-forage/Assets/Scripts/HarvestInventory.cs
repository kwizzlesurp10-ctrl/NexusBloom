using UnityEngine;
using System.Collections.Generic;

public class HarvestInventory : MonoBehaviour {
    public static HarvestInventory Instance;
    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    [SerializeField] private string[] harvestTips = { "Wash thoroughly; urban dust lingers.", "Pair with lemon for sustainability zing." };

    private void Awake() { Instance = this; }

    public void HarvestPlant(string plantName) {
        inventory[plantName] = inventory.ContainsKey(plantName) ? inventory[plantName] + 1 : 1;
        Debug.Log($"Harvested {plantName}! Tip: {harvestTips[Random.Range(0, harvestTips.Length)]}");
    }

    public int GetCount(string plantName) => inventory.ContainsKey(plantName) ? inventory[plantName] : 0;
}