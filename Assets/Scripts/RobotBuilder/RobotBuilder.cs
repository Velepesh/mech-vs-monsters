using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RobotBuilder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<ChooseLimbButton> _limbButtons;
    [SerializeField] private List<Head> _heads;
    [SerializeField] private List<Arm> _arms;
    [SerializeField] private List<Body> _bodies;
    [SerializeField] private List<Leg> _legs;//загрузка изначально здоровья

    private const string HEAD_ID = "HeadID";
    private const string ARM_ID = "ArmID";
    private const string BODY_ID = "BodyID";
    private const string LEG_ID = "LegID";

    private readonly int _defaultIndex = -1;
    private int _currentHeadIndex => PlayerPrefs.GetInt(HEAD_ID, _defaultIndex);
    private int _currentArmIndex => PlayerPrefs.GetInt(ARM_ID, _defaultIndex);
    private int _currentBodyIndex => PlayerPrefs.GetInt(BODY_ID, _defaultIndex);
    private int _currentLegIndex => PlayerPrefs.GetInt(LEG_ID, _defaultIndex);

    public bool IsArmSelected => _currentArmIndex > _defaultIndex;
   
    public event UnityAction BodySelected;

    private void OnEnable()
    {
        for (int i = 0; i < _limbButtons.Count; i++)
            _limbButtons[i].LimbSelected += OnLimbSelected;        
    }

    private void Start()
    {
        Load();
    }

    private void OnDisable()
    {
        for (int i = 0; i < _limbButtons.Count; i++)
            _limbButtons[i].LimbSelected -= OnLimbSelected;
    }

    public void SelectLimb(LimbType type, int index)
    {
        if (type == LimbType.HEAD)
            ChangeCurrentLimb(HEAD_ID, index, _heads);
        else if (type == LimbType.ARM)
            ChangeCurrentLimb(ARM_ID, index, _arms);
        else if (type == LimbType.BODY)
            ChangeCurrentLimb(BODY_ID, index, _bodies);
        else if (type == LimbType.LEG)
            ChangeCurrentLimb(LEG_ID, index, _legs);
    }

    public void TakeAdditionalWeapon(PlayerAdditionalWeapon weapon)
    {
        weapon.gameObject.SetActive(true);
    }

    private void Load()
    {
        TryLockButtons();
        TryWear();
    }

    private void LoadHealth(PlayerLimb limb)
    {
        _player.AddHealth(limb.Health);
    }

    private void TryWear()
    {
        if (_currentHeadIndex > _defaultIndex)
            LoadLimb(_heads[_currentHeadIndex]);

        if (_currentArmIndex > _defaultIndex) 
            LoadLimb(_arms[_currentArmIndex]);

        if (_currentBodyIndex > _defaultIndex)
        {
            LoadLimb(_bodies[_currentBodyIndex]);

            UnlockButton<HeadButton>();
            UnlockButton<ArmButton>();

            BodySelected?.Invoke();
        }

        if (_currentLegIndex > _defaultIndex)
        {
            LoadLimb(_legs[_currentLegIndex]);
            UnlockButton<BodyButton>();
        }
    }

    private void LoadLimb(PlayerLimb limb)
    {
        ApplyNewLimb(limb);
        LoadHealth(limb);
    }

    private void ChangeCurrentLimb(string name, int index, IReadOnlyList<PlayerLimb> playerLimbs)
    {
        RemovePastLimb(playerLimbs);
        ApplyNewLimb(playerLimbs[index]);
        SaveCurrentTemplateIndex(name, index);
    }

    private void RemovePastLimb(IReadOnlyList<PlayerLimb> playerLimbs)
    {
        for (int i = 0; i < playerLimbs.Count; i++)
        {
            if (playerLimbs[i].gameObject.activeSelf)
                playerLimbs[i].gameObject.SetActive(false);
        }
    }

    private void ApplyNewLimb(PlayerLimb limb)
    {
        if (limb is Head head)
            head.EnableCollider();

        if (limb is Arm arm)
            arm.EnableCollider();

        limb.Limb.Selecte();
        limb.gameObject.SetActive(true);
    }

    private void TryLockButtons()
    {
        for (int i = 0; i < _limbButtons.Count; i++)
        {
            if (_limbButtons[i] is LegButton == false)
                _limbButtons[i].Lock();
        }
    }

    private void OnLimbSelected(ChooseLimbButton limbButtons)
    {
        UnlockButton(limbButtons);
    }

    private void UnlockButton(ChooseLimbButton limbButtons)
    {
        if (limbButtons is LegButton)
        {
            UnlockButton<BodyButton>();
        }
        else if (limbButtons is BodyButton)
        {
            UnlockButton<HeadButton>();
            UnlockButton<ArmButton>();

            BodySelected?.Invoke();
        }
    }
    private void UnlockButton<ChooseLimbButton>()
    {
        for (int i = 0; i < _limbButtons.Count; i++)
        {
            if (_limbButtons[i] is ChooseLimbButton)
                _limbButtons[i].Unlock();
        }
    }

    private void SaveCurrentTemplateIndex(string name, int index)
    {
        PlayerPrefs.SetInt(name, index);
    }
}