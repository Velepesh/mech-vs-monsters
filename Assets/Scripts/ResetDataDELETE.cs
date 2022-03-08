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

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("CurrentLevelID", 20);
            Debug.Log("Level " + PlayerPrefs.GetInt("CurrentLevelID"));
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("CurrentLevelID", 1);
            Debug.Log("Level " + PlayerPrefs.GetInt("CurrentLevelID"));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseMoney();
            Debug.Log("IncreaseMoney");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ResetMoney();
            Debug.Log("ResetMoney");
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
        PlayerPrefs.SetInt("MiniDroneLeft", 0);
        PlayerPrefs.SetInt("MinigunLeft", 0);
        PlayerPrefs.SetInt("MinigunRight", 0);
        PlayerPrefs.SetInt("RocketgunLeft", 0);
        PlayerPrefs.SetInt("RocketgunRight", 0);
        PlayerPrefs.SetInt("Arm_1", 0);
        PlayerPrefs.SetInt("Arm_2", 0);
        PlayerPrefs.SetInt("Arm_3", 0);
        PlayerPrefs.SetInt("Arm_4", 0);
        PlayerPrefs.SetInt("Arm_5", 0);
        PlayerPrefs.SetInt("Body_1", 0);
        PlayerPrefs.SetInt("Body_2", 0);
        PlayerPrefs.SetInt("Body_3", 0);
        PlayerPrefs.SetInt("Body_4", 0);
        PlayerPrefs.SetInt("Body_5", 0);
        PlayerPrefs.SetInt("Head_1", 0);
        PlayerPrefs.SetInt("Head_2", 0);
        PlayerPrefs.SetInt("Head_3", 0);
        PlayerPrefs.SetInt("Head_4", 0);
        PlayerPrefs.SetInt("Head_5", 0);
        PlayerPrefs.SetInt("Leg_1", 0);
        PlayerPrefs.SetInt("Leg_2", 0);
        PlayerPrefs.SetInt("Leg_3", 0);
        PlayerPrefs.SetInt("Leg_4", 0);
        PlayerPrefs.SetInt("Leg_5", 0);
    }

    private void IncreaseMoney()
    {
        PlayerPrefs.SetInt("Balance", 300);
    }

    private void ResetMoney()
    {
        PlayerPrefs.SetInt("Balance", 0);
    }
}