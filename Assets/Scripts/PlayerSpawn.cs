using UnityEngine;
using Fusion;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    public GameObject controllerPrefab;
    public Transform playerSpawnPosition;
    public Transform controllerLocation;
    
    public void PlayerJoined(PlayerRef player)
    {
        // if (player == Runner.LocalPlayer)
        // {
        //     Vector3 spawnPosition = playerSpawnPosition.position;
        //     Runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
        // }

        if (Runner.IsServer)
        {
            Vector3 spawnPosition = playerSpawnPosition.position;
            Runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
        }

        if (player == Runner.LocalPlayer)
        {
            Instantiate(controllerPrefab, controllerLocation.position, controllerLocation.rotation);
        }
    }
}
