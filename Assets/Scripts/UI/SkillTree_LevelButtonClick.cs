using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class SkillTree_LevelButtonClick : MonoBehaviour
{
    //00FF75
    public Color unavailableColor;
    public Color inActiveColor;
    public Color availableColor;
    [SerializeField]
    private GlobalEnumsList.LevelButtonState ButtonState;

    private string sActiveImageLayer = "activeImageLayer";
    private Image activeImageLayer;

    private Button levelbutton;
    private TextMeshProUGUI skillSetLevelDescription;
    private string description;

    private SkillTreeData skillTreeData;


    Action<object> skillPointsUpdateAction;

    #region Unity Methods
    void Awake()
    {

        skillPointsUpdateAction = EventManager.Convert<int>(SkillPointsUpdatedDelegate);
        EventManager.StartListening(GlobalEventsNameList.SKILL_POINTS_UPDATE, skillPointsUpdateAction);
        levelbutton = GetComponent<Button>();
        //MTM - this is the image overlay which visually changes from dark overlay to no overlay, so bright and active
        activeImageLayer = GetComponent<Image>();
        skillSetLevelDescription = GetComponentInChildren<TextMeshProUGUI>();
        skillTreeData = GameObject.FindObjectOfType<SkillTreeData>();
        //keep the initial state unavailable, so color of the activeImageLayer also unavailable.
    }

    void Start()
    {


        levelbutton.onClick.AddListener(OnClick);


        skillSetLevelDescription.text = (string)skillTreeData.GetSkillSetLevelFields(transform.GetSiblingIndex(), (transform.parent.GetSiblingIndex() / 2), transform.parent.parent.name, "description");

        ///Check if activated is true based on initial json data parsed in skilltree
        //, otherwise keep it unavailable. The inactive state will be handled later through the skillpoint update
        changeState(skillTreeData.getCurrentState(transform.GetSiblingIndex(), (transform.parent.GetSiblingIndex() / 2), transform.parent.parent.name));



    }

    void OnDestroy()
    {
        levelbutton.onClick.RemoveAllListeners();
        // EventManager.StopListening(GlobalEventsNameList.SKILL_POINTS_UPDATE, skillPointsUpdateAction);
    }
    #endregion

    #region Control Methods
    void OnClick()
    {
        if (ButtonState == GlobalEnumsList.LevelButtonState.inactive)
        {
            skillTreeData.UpdateActivation(transform.GetSiblingIndex(), (transform.parent.GetSiblingIndex() / 2), transform.parent.parent.name);

            changeState(GlobalEnumsList.LevelButtonState.active);

        }


    }
    #endregion

    #region Event Callback Methods
    public void SkillPointsUpdatedDelegate(int skillpoints)
    {
        changeState(skillTreeData.getCurrentState(transform.GetSiblingIndex(), (transform.parent.GetSiblingIndex() / 2), transform.parent.parent.name));

    }
    #endregion

    #region Misc
    void changeState(GlobalEnumsList.LevelButtonState state)
    {
        ButtonState = state;
        switch (state)
        {
            case GlobalEnumsList.LevelButtonState.active:
                activeImageLayer.color = availableColor;
                break;
            case GlobalEnumsList.LevelButtonState.inactive:
                activeImageLayer.color = inActiveColor;
                break;
            case GlobalEnumsList.LevelButtonState.unavailable:
                activeImageLayer.color = unavailableColor;
                break;
        }
    }
    #endregion
}
