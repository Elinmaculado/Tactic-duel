using UnityEngine;
using Fusion;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    public Transform playerSpawnPosition;
    
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Vector3 spawnPosition = playerSpawnPosition.position;
            Runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
        }
    }
}
