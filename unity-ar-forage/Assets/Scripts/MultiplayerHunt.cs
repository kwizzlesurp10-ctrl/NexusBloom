using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerHunt : MonoBehaviourPunObserver {
    [SerializeField] private string roomName = "UrbanForageHub";
    private List<Vector3> sharedScans = new List<Vector3>();

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, null);
    }

    [PunRPC] public void ShareScanPosition(Vector3 position) {
        sharedScans.Add(position);
        GameObject marker = Instantiate(/* sharedPrefab */, position, Quaternion.identity);
        PhotonNetwork.Destroy(marker, 30f);
    }

    public void BroadcastHarvest(Vector3 harvestPos) { photonView.RPC("ShareScanPosition", RpcTarget.All, harvestPos); }
}