using System;
using UnityEngine;
using System.Collections.Generic;

public class AmplitudeAnalytics : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private const string SAVED_REG_DAY = "RegDaySave";
    private const string SAVED_REG_DAY_FULL = "RegDaySaveFull";
    private const string SAVED_SESSION_ID = "SessionID";

    private Game _game;
    private int _regDayCounter => PlayerPrefs.GetInt("RegDayCounter", 1);

    private Amplitude _amplitude;

    private string _regDay
    {
        get { return PlayerPrefs.GetString(SAVED_REG_DAY, DateTime.Today.ToString("dd/MM/yy")); }
        set { PlayerPrefs.SetString(SAVED_REG_DAY, value); }
    }

    private string _regDayFull
    {
        get { return PlayerPrefs.GetString(SAVED_REG_DAY_FULL, DateTime.Today.ToString()); }
        set { PlayerPrefs.SetString(SAVED_REG_DAY_FULL, value); }
    }

    private int _sessionCount
    {
        get { return PlayerPrefs.GetInt(SAVED_SESSION_ID, 0); }
        set { PlayerPrefs.SetInt(SAVED_SESSION_ID, value); }
    }

    private void Awake()
    {
        _game = GetComponent<Game>();

        _amplitude = Amplitude.Instance;
        _amplitude.logging = true;
        _amplitude.init("f8bee7af8cc2eb58ce2afbe81f29042d");
    }

    private void OnEnable()
    {
        _wallet.Bought += OnBought;
        _game.LevelStarted += OnLevelStarted;
        _game.LevelWon += OnLevelWon;
        _game.LevelLost += OnLevelLost;
        _game.LevelRestart += OnLevelRestart;
        _game.LevelMainMenu += OnLevelMainMenu;
    }

    private void OnDisable()
    {
        _wallet.Bought += OnBought;
        _game.LevelStarted -= OnLevelStarted;
        _game.LevelWon -= OnLevelWon;
        _game.LevelLost -= OnLevelLost;
        _game.LevelMainMenu -= OnLevelMainMenu;
    }

    public void GameStart()
    {
        if (_game.CurrentLevel == 1 && _regDayCounter == 1)
        {
            _regDay = DateTime.Today.ToString("dd/MM/yy");
            _regDayFull = DateTime.Today.ToString();
            _amplitude.setOnceUserProperty("reg_day", _regDay);

            PlayerPrefs.SetInt("RegDayCounter", 100);
        }

        SetBasicProperty();
        SettingUserProperties();

        FireEvent("game_start", "count", _sessionCount);
    }

    private void OnBought(string type, string name, int amount)
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();

        eventProps.Add("type", type);
        eventProps.Add("name", name);
        eventProps.Add("amount", amount);

        _amplitude.logEvent("soft_spent", eventProps);
    }

    private void OnLevelLost()
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();

        eventProps.Add("level", _game.CurrentLevel);
        eventProps.Add("time_spent", _game.SpentTime);

        _amplitude.logEvent("fail", eventProps);
    }

    private void OnLevelMainMenu()
    {
        _amplitude.logEvent("main_menu");
    }

    private void OnLevelRestart()
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();

        eventProps.Add("level", _game.CurrentLevel);

        _amplitude.logEvent("restart", eventProps);
    }


    private void OnLevelWon()
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();

        eventProps.Add("level", _game.CurrentLevel);
        eventProps.Add("time_spent", _game.SpentTime);

        _amplitude.logEvent("level_complete", eventProps);
    }

    private void OnLevelStarted()
    {
        FireEvent("level_start", "level", _game.CurrentLevel);
    }

    private void SetBasicProperty()
    {
        _sessionCount = _sessionCount + 1;
        _amplitude.setUserProperty("session_count", _sessionCount);

        int daysInGame = DateTime.Today.Subtract(DateTime.Parse(_regDayFull)).Days;
        _amplitude.setUserProperty("days_in_game", daysInGame);
    }

    private void FireEvent(string eventName, string properties, object type)
    {
        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add(properties, type);
        _amplitude.logEvent(eventName, eventProps);
    }

    private void SettingUserProperties()
    {
        int lastLevel = _game.CurrentLevel;
        _amplitude.setUserProperty("last_level", lastLevel);
    }
}
