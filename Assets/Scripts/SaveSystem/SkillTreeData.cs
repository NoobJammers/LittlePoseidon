using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

public class SkillTreeData : MonoBehaviour
{
    //TODO: Reference for application class (some class that controls exit, entry and other things maybe)
    //using Jobject rather than a class model because it will make it easier to add new skills.
    //eg. json skilltree file {spiral:{0:{0:{properties},1:{properties}}}, tornado:{}, wave:{}}
    //to add new just need to add {spiral:{0:{0:{properties},1:{properties}}}, tornado:{}, wave:{}, newpower:{}}
    //we have SkillTreeProperties_Spiral with some properties assembled from the merging of innerjson objects the 5 types of skillset
    //so, damage+TTR+TTL....=combined properties file -> will be parsed into the SkillTreeProperties class.
    //so even if we need to add a new skillset like say spiral color then we add that field to the SkillTreeProperties class
    // and then damage+TTR+TTL...+color=combined properties file is parsed to SkillTreePropertiesFile.

    //if we make a skilltreedata class with nested classes such that class skilltrees{skilltree spiral; skilltree tornado; ...}
    //and then inside skilltree spiral we have : array of skillsets
    // and inside skillset we have the 5 different levels. 
    // too much work. plus already existing save file might get corrupted on update.

    //seems straightforward to work with the jsonobject
    JObject skillTreeData;
    PlayerDataController playerData;
    // public Type type;
    void Awake()
    {
        LoadSkillData();
        playerData = GameObject.FindObjectOfType<PlayerDataController>();
    }
    void Start()
    {

    }
    public SkillTreeProperties GetPropertiesObject(string power)
    {
        if (skillTreeData != null)
        {
            JObject finalPropertiesObject = new JObject();
            JArray skillset = (JArray)(skillTreeData[power]);
            for (int i = 0; i < skillset.Count; i++)
            {

                JArray levels = (JArray)(skillset[i]["levels"]);
                int j = 0;
                for (j = 0; j < levels.Count; j++)
                {
                    JObject level = (JObject)(levels[j]);
                    if (!level["activated"].Value<bool>())
                    {
                        break;
                    }

                }
                JObject propertiesObject = (JObject)(levels[j - 1]["properties"]);
                finalPropertiesObject.Merge(propertiesObject);


            }
            return finalPropertiesObject.ToObject<SkillTreeProperties>();

        }
        else
        {
            Debug.LogError("skillTreeData empty in SkillTreeData class- GetPropertiesObject");
            return null;
        }
    }
    public void LoadSkillData()
    {
        string folderPath = Application.persistentDataPath;
        string fileName = "skilltreedata.json";
        string filePath = folderPath + "/" + fileName;


        try
        {
            string jsonData = File.ReadAllText(filePath);
            Debug.Log("jdata: " + jsonData);

            skillTreeData = JObject.Parse(jsonData);
            // GetPropertiesObject("myList");

        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);

            //TODO: CALL EXIT OF APPLICATION USING APPLICATION CLASS REFERENCE WITH - " SKILL TREE DATA CORRUPTED" MESSAGE
        }



    }
    public void SaveSkillData()
    {
        string folderPath = Application.persistentDataPath;
        string fileName = "skilltreedata.json";
        string filePath = folderPath + "/" + fileName;


        try
        {
            string skillDataString = skillTreeData.ToString();
            File.WriteAllText(filePath, skillDataString);

        }
        catch (Exception ex)
        {
            //TODO: RAISE ERROR "COULD NOT SAVE, CORRUPTED SKILLDATA"
        }



    }
    public JToken GetSkillSetLevelFields(int skillSetLevelIndex, int skillSetIndex, string skillTree, string property)
    {
        Debug.Log("indices skillsetindex:" + skillSetIndex + " skillsetlevelindex:" + skillSetLevelIndex);
        if (skillTreeData != null)
        {
            return GetLevelObject(skillSetLevelIndex, skillSetIndex, skillTree)[property];
        }
        else
        {
            Debug.Log("error:skilltree is null - GetActivationStatus");
            return null;
        }
    }
    public JToken GetSkillSetFields(int skillSetIndex, string skillTree, string property)
    {
        if (skillTreeData != null)
        {
            return GetSkillSetObject(skillSetIndex, skillTree)[property];
        }
        else
        {
            Debug.Log("error:skilltree is null- GetLevelDescription");
            return null;
        }
    }
    public JObject GetSkillSetObject(int skillSetIndex, string skillTree)
    {
        try
        {

            // Debug.Log((JObject)((JArray)skillTreeData[skillTree])[skillSetIndex]);
            return (JObject)((JArray)skillTreeData[skillTree])[skillSetIndex];
        }
        catch (System.NullReferenceException)
        { return null; }
    }
    public JObject GetLevelObject(int skillSetLevelIndex, int skillSetIndex, string skillTree)
    {
        try
        {
            // Debug.Log((JObject)GetSkillSetObject(skillSetIndex, skillTree)["levels"]);
            return (JObject)((JArray)GetSkillSetObject(skillSetIndex, skillTree)["levels"])[skillSetLevelIndex];
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    public void UpdateLevelFields(int skillSetLevelIndex, int skillSetIndex, string skillTree, string property, string value)
    {
        try
        {
            GetLevelObject(skillSetLevelIndex, skillSetIndex, skillTree)[property] = value;
        }
        catch (System.Exception)
        {
            Debug.Log("UpdateLevelDields error");
        }


    }
    public void UpdateActivation(int skillSetLevelIndex, int skillSetIndex, string skillTree)
    {
        // try
        // {


        UpdateLevelFields(skillSetLevelIndex, skillSetIndex, skillTree, "activated", "true");
        playerData.ConsumeSkillPoints((int)GetSkillSetLevelFields(skillSetLevelIndex, skillSetIndex, skillTree, "required_skillpoints"));




        // }
        // catch (
        //     System.NullReferenceException
        // )
        // {
        //     Debug.Log("Error-Try Update Activation, null reference exception");
        //     return false;

        // }

        //TODO: CALL PROPERTIES OBJECT AND RAISE EVENT WHERE YOU PASS THE PROPERTIES OBJECT
    }

    public bool isActivated(int skillSetLevelIndex, int skillSetIndex, string skillTree)
    {
        return (bool)GetSkillSetLevelFields(skillSetLevelIndex, skillSetIndex, skillTree, "activated");

    }

    public GlobalEnumsList.LevelButtonState getCurrentState(int skillSetLevelIndex, int skillSetIndex, string skillTree)
    {
        if (isActivated(skillSetLevelIndex, skillSetIndex, skillTree))
        { return GlobalEnumsList.LevelButtonState.active; }
        if (isActivated(skillSetLevelIndex - 1, skillSetIndex, skillTree))
        {
            int skillPointsRequired = (int)GetSkillSetLevelFields(skillSetLevelIndex, skillSetIndex, skillTree, "required_skillpoints");
            if (playerData.GetSkillPoints() >= skillPointsRequired)
            {
                return GlobalEnumsList.LevelButtonState.inactive;
            }
            else
            {
                return GlobalEnumsList.LevelButtonState.unavailable;
            }
        }
        else
        {
            return GlobalEnumsList.LevelButtonState.unavailable;
        }
    }

    void OnDestroy()
    {
        SaveSkillData();
    }


}
