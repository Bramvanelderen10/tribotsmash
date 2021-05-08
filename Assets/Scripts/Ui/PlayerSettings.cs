using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tribot
{
    /// <summary>
    /// Holds player settings and loads/saves them to file
    /// </summary>
    public class PlayerSettings : Singleton<PlayerSettings>
    {
        private List<string> _defaultNames = new List<string>() { "Piet", "Jan", "Kees", "Gert", "Blitz", "IRobot", "Wall-E" };
        public Action OnNameChange;


        public string PlayerName
        {
            get
            {
                return (PlayerPrefs.HasKey("playername"))
                    ? PlayerPrefs.GetString("playername")
                    : _defaultNames[UnityEngine.Random.Range(0, _defaultNames.Count)];
            }
        }

        public string RandomName
        {
            get { return _defaultNames[UnityEngine.Random.Range(0, _defaultNames.Count)]; }
        }

        public PlayerSettings()
        {
            if (PlayerPrefs.HasKey("playername"))
                SetName(PlayerPrefs.GetString("playername"));
            else
                OpenDialog();
        }

        public void SetName(string name)
        {
            PlayerPrefs.SetString("playername", name);
            PhotonNetwork.player.NickName = name;

            if (OnNameChange != null)
                OnNameChange();
        }

        public void OpenDialog()
        {
            var dialogObj = GameObject.Instantiate(Resources.Load<GameObject>("PlayerDialog"));
            if (!dialogObj)
            {
                SetName(PlayerName);
            }
            else
            {
                var playerDialog = dialogObj.GetComponent<PlayerDialog>();
                playerDialog.Callback = SetName;
                playerDialog.Name = PlayerName;
            }
        }
    }
}

