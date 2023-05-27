using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MatchOptions matchOpts;

    public static GameManager instance;

    void Awake() {
        if (instance != null) Debug.LogError("More than one instance of GameManager!");
        else instance = this;
    }

    // ID prefix
    private const string ID_PREFIX = "Player ";

    // player list
    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    // adds player to player list
    public static void RegisterPlayer(string id, Player player) {
        players.Add(ID_PREFIX + id, player);
        player.transform.name = ID_PREFIX + id;
    }

    // removes player from player list
    public static void UnregisterPlayer(string id) => players.Remove(id);

    // finds and returns player
    public static Player GetPlayer(string id) => players[id];
}
