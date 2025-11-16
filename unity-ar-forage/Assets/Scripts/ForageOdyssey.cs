public class ForageOdyssey : ForageGameManager {
    [SerializeField] private ARPlantScanner scanner;
    [SerializeField] private PlantInfoPanel infoPanel;
    [SerializeField] private HarvestInventory inventory;
    [SerializeField] private MultiplayerHunt multiplayer;
    [SerializeField] private ProgressionSystem progression;

    private void Start() {
        // Wire events
        progression.UnlockSeasonalEvent("ForageBlitz");
    }
}