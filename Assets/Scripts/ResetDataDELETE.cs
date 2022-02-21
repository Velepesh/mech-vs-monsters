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
    }

    private void IncreaseMoney()
    {
        PlayerPrefs.SetInt("Balance", 100000);
    }

    private void ResetMoney()
    {
        PlayerPrefs.SetInt("Balance", 0);
    }
}