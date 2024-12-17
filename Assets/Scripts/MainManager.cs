using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    // Dictionary to track item given status for each NPC by their unique ID
    public Dictionary<string, bool> npcItemGivenStatus = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool IsItemGivenToNPC(string npcID)
    {
        return npcItemGivenStatus.ContainsKey(npcID) && npcItemGivenStatus[npcID];
    }

    public void SetItemGivenToNPC(string npcID, bool status)
    {
        npcItemGivenStatus[npcID] = status;
    }
}

