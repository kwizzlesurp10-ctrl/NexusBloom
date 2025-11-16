using UnityEngine;
using UnityEngine.Purchasing;

public class ProgressionSystem : MonoBehaviour, IStoreListener {
    [SerializeField] private int levelThreshold = 50;
    private int currentLevel = 1;
    private string premiumPack = "eco_seed_pack";

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {}

    public void UnlockSeasonalEvent(string eventId) {
        if (currentLevel >= levelThreshold || HasPurchasedPremium()) {
            Debug.Log($"Event Unlocked: {eventId}");
        }
    }

    private bool HasPurchasedPremium() { return false; }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
        if (args.purchasedProduct.definition.id == premiumPack) UnlockSeasonalEvent("UrbanMythHunt");
        return PurchaseProcessingResult.Complete;
    }
}