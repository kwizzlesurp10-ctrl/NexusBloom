using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "PlantData", menuName = "Forage/PlantData")]
public class PlantData : ScriptableObject {
    public string plantName = "Dandelion";
    public string[] facts = { "Edible greens: Nutritious vitamin boost.", "Risk: Avoid if allergic to ragweed." };
    public bool isToxic = false;
}

public class PlantInfoPanel : MonoBehaviour {
    [SerializeField] private PlantData currentPlant;
    [SerializeField] private TextMeshProUGUI factText;
    private int factIndex = 0;

    public void DisplayFacts(PlantData plant) {
        currentPlant = plant;
        factText.text = currentPlant.facts[factIndex];
        factIndex = (factIndex + 1) % currentPlant.facts.Length;
        if (currentPlant.isToxic) factText.color = Color.red;
    }
}