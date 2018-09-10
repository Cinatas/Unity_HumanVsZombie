using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateJson;

public class PlayerInfomation{

    PlayerData data;

    public bool LoadPlayerData()
    {
        return true;
    }

    public bool SavePlayerData()
    {
        return true;
    }

	void LoadPlayerDataFromJson(string jsonData)
    {
        data = JsonObject.Deserialise<PlayerData>(jsonData);
    }

    void SavePlaerDataToJson()
    {

    }
}
