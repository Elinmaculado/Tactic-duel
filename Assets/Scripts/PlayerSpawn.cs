using UnityEngine;
using Fusion;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    public Transform playerSpawnPosition;
    public bool spawned = false;
    
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer && !spawned)
        {
            spawned = true;
            Vector3 spawnPosition = playerSpawnPosition.position;
            
            var skibidi = Runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
            
            Debug.Log(skibidi.name);
        }
    }
}
