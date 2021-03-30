using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
public class PlayerDataController : MonoBehaviour
{
    PlayerData playerData;

    #region Unity Methods
    void Awake()
    {
        LoadPlayerData();
    }

    void OnDestroy()
    {
        SavePlayerData();
    }

    void Start()
    {
        // EventManager.TriggerEvent(GlobalEventsNameList.SKILL_POINTS_UPDATE, playerData.SkillPoints);
    }
    #endregion

    #region Control Methods
    public void LoadPlayerData()
    {
        string folderPath = Application.persistentDataPath;
        string fileName = "playerdata.json";
        string filePath = folderPath + "/" + fileName;


        try
        {
            string jsonData = File.ReadAllText(filePath);
            Debug.Log("jdata: " + jsonData);

            playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            // GetPropertiesObject("myList");

        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);

            //TODO: CALL EXIT OF APPLICATION USING APPLICATION CLASS REFERENCE WITH - " PLAYER DATA CORRUPTED" MESSAGE
        }



    }
    public void SavePlayerData()
    {
        string folderPath = Application.persistentDataPath;
        string fileName = "playerdata.json";
        string filePath = folderPath + "/" + fileName;


        try
        {
            string skillDataString = JsonConvert.SerializeObject(playerData).ToString();
            File.WriteAllText(filePath, skillDataString);

        }
        catch (Exception ex)
        {
            //TODO: RAISE ERROR "COULD NOT SAVE, CORRUPTED PLAYERDATA"
        }



    }

    public int GetSkillPoints()
    {

        return playerData.SkillPoints;
    }

    public void ConsumeSkillPoints(int skillPoints)
    {
        playerData.SkillPoints -= skillPoints;
        EventManager.TriggerEvent(GlobalEventsNameList.SKILL_POINTS_UPDATE, playerData.SkillPoints);
        //TODO: RAISE EVENT NOTIFYING SKILL POINTS UPDATE
    }

    public void UpdateXP(int XP)
    {
        if (playerData.XP + XP >= playerData.XPToSkillPointThreshold)
        {
            playerData.XP = 0;
            playerData.SkillPoints += 1;
            EventManager.TriggerEvent(GlobalEventsNameList.SKILL_POINTS_UPDATE, playerData.SkillPoints);
        }
        else
        {
            playerData.XP += XP;
        }
    }


    #endregion

    #region Event System Methods

    #endregion

}