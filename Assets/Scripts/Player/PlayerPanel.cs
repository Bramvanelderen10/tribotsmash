using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tribot;
using System;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private List<ButtonInfo> _buttons = new List<ButtonInfo>();

    private Player _player;

    [Serializable]
    private class ButtonInfo
    {
        public TribotInput.InputButtons Button;
        public Image Icon;
        public TextMeshProUGUI ButtonName;
        public bool Active = false;
    }

    private Dictionary<TribotInput.InputButtons, ButtonInfo> _abilityButtonInfo;

    void Start()
    {
        //Add all buttons to dictionary for quicker lookup
        _abilityButtonInfo = new Dictionary<TribotInput.InputButtons, ButtonInfo>();
        foreach (var item in _buttons)
        {
            _abilityButtonInfo.Add(item.Button, item);
        }
    }

    void Update()
    {
        if (!_player)
        {
            gameObject.SetActive(false);
            return;
        }

        /*
        The cleanest less performant way would be looping through _abilityButtonInfo then find the ability with it
        which would be o(n + n * m)
        which can be 8 * 8 + 8 at maximum so that means 64

        While this triple loop can only reach 8 + 8 + 8 at maximum so that means 24   3 times o(n)
        */

        //o(n) Max 8 times
        foreach (var item in _abilityButtonInfo)
        {
            item.Value.Active = false;
        }

        //o(n) Max 8 times
        foreach (var ability in _player.Abilities)
        {
            if (_abilityButtonInfo[ability.Key].Icon.sprite != ability.Value.UiIcon)
                _abilityButtonInfo[ability.Key].Icon.sprite = ability.Value.UiIcon;
            _abilityButtonInfo[ability.Key].Icon.fillAmount = 1f - ability.Value.GetCooldown;
            _abilityButtonInfo[ability.Key].Active = true;
        }
        

        //o(n) Max 8 times
        foreach (var item in _abilityButtonInfo)
        {
            if (!item.Value.Active)
            {
                item.Value.Icon.fillAmount = 0f;
            }
        }
    }

    public void SetPlayer(IPlayerManager player)
    {
        gameObject.SetActive(true);
        _player = player.PlayerObject.GetComponent<Player>();
        _name.text = player.Name;
    }
}
