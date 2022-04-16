using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetDataDELETE : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveCurrentTemplateIndex();
            Debug.Log("Template Reset");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("CurrentLevelID", 1);
            Debug.Log(PlayerPrefs.GetInt("CurrentLevelID"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("CurrentLevelID", 2);
            Debug.Log(PlayerPrefs.GetInt("CurrentLevelID"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("CurrentLevelID", 3);
            Debug.Log(PlayerPrefs.GetInt("CurrentLevelID"));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseMoney();
            Debug.Log("IncreaseMoney");
        }
    }

    private void SaveCurrentTemplateIndex()
    {
        PlayerPrefs.SetInt("HeadID", -1);
        PlayerPrefs.SetInt("ArmID", -1);
        PlayerPrefs.SetInt("BodyID", -1);
        PlayerPrefs.SetInt("LegID", -1);
        PlayerPrefs.SetInt("Tutorial", 1);
        PlayerPrefs.SetInt("BigDrone", 0);
        PlayerPrefs.SetInt("KneeGunLeft", 0);
        PlayerPrefs.SetInt("KneeGunRight", 0);
        PlayerPrefs.SetInt("MiniDroneLeft", 0);
        PlayerPrefs.SetInt("MiniDroneRight", 0);
        PlayerPrefs.SetInt("MinigunLeft", 0);
        PlayerPrefs.SetInt("MinigunRight", 0);
        PlayerPrefs.SetInt("RocketgunLeft", 0);
        PlayerPrefs.SetInt("RocketgunRight", 0);
        PlayerPrefs.SetInt("Arm_1", 0);
        PlayerPrefs.SetInt("Arm_2", 0);
        PlayerPrefs.SetInt("Arm_3", 0);
        PlayerPrefs.SetInt("Arm_4", 0);
        PlayerPrefs.SetInt("Arm_5", 0);
        PlayerPrefs.SetInt("Body_1", 1);
        PlayerPrefs.SetInt("Body_2", -1);
        PlayerPrefs.SetInt("Body_3", -1);
        PlayerPrefs.SetInt("Body_4", -1);
        PlayerPrefs.SetInt("Body_5", -1);
        PlayerPrefs.SetInt("Head_1", -1);
        PlayerPrefs.SetInt("Head_2", -1);
        PlayerPrefs.SetInt("Head_3", -1);
        PlayerPrefs.SetInt("Head_4", -1);
        PlayerPrefs.SetInt("Head_5", -1);
        PlayerPrefs.SetInt("Leg_1", 1);
        PlayerPrefs.SetInt("Leg_2", -1);
        PlayerPrefs.SetInt("Leg_3", -1);
        PlayerPrefs.SetInt("Leg_4", -1);
        PlayerPrefs.SetInt("Leg_5", -1);
        PlayerPrefs.SetInt("Arm_1Select", -1);
        PlayerPrefs.SetInt("Arm_2Select", -1);
        PlayerPrefs.SetInt("Arm_3Select", -1);
        PlayerPrefs.SetInt("Arm_4Select", -1);
        PlayerPrefs.SetInt("Arm_5Select", -1);
        PlayerPrefs.SetInt("Body_1Select", -1);
        PlayerPrefs.SetInt("Body_2Select", -1);
        PlayerPrefs.SetInt("Body_3Select", -1);
        PlayerPrefs.SetInt("Body_4Select", -1);
        PlayerPrefs.SetInt("Body_5Select", -1);
        PlayerPrefs.SetInt("Head_1Select", -1);
        PlayerPrefs.SetInt("Head_2Select", -1);
        PlayerPrefs.SetInt("Head_3Select", -1);
        PlayerPrefs.SetInt("Head_4Select", -1);
        PlayerPrefs.SetInt("Head_5Select", -1);
        PlayerPrefs.SetInt("Leg_1Select", -1);
        PlayerPrefs.SetInt("Leg_2Select", -1);
        PlayerPrefs.SetInt("Leg_3Select", -1);
        PlayerPrefs.SetInt("Leg_4Select", -1);
        PlayerPrefs.SetInt("Leg_5Select", -1);
        ResetMoney();
        PlayerPrefs.SetInt("CurrentLevelID", 1);
        PlayerPrefs.SetInt("LastOpenedShopID", -1);
    }

    private void IncreaseMoney()
    {
        PlayerPrefs.SetInt("Balance", 999999);
    }

    private void ResetMoney()
    {
        PlayerPrefs.SetInt("Balance", 0);
    }
}