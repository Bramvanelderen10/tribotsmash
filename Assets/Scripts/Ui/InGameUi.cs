using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tribot;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private List<PlayerPanel> _playerPanels = new List<PlayerPanel>
    {
        null,
        null,
        null,
        null,
    };

    public void AddPlayer(IPlayerManager player)
    {
        _playerPanels[player.Index].SetPlayer(player);
    }
}
