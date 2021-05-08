using UnityEngine;
using UnityEngine.UI;
using Photon;
using TMPro;
using System;

[RequireComponent(typeof(PhotonView))]
public class CharacterSelectManager : PunBehaviour
{
    public IterateSkin IterateSkinCallback;
    public CharacterIndex[] SelectedCharacters;

    [SerializeField] private TextMeshProUGUI[] CharacterNames = new TextMeshProUGUI[4]; //1 for each player
    [SerializeField] private TextMeshProUGUI[] CharacterSkinNames = new TextMeshProUGUI[4]; //1 for each player
    private CharacterData _charData;
    [SerializeField] private PlayerButtons[] _playerButtons = new PlayerButtons[4];
    [Serializable] private class PlayerButtons
    {
        public GameObject Left;
        public GameObject Right;
    }

    void Awake()
    {
        _charData = Resources.Load<CharacterData>("CharacterData");
        SelectedCharacters = new CharacterIndex[4];
        for (int i = 0; i < 4; i++)
        {
            var charIndex = new CharacterIndex();
            charIndex.ModelIndex = 0;
            charIndex.SkinIndex = UnityEngine.Random.Range(0, _charData.Characters[0].Skins.Count);
            SelectedCharacters[i] = charIndex;
        }

        foreach (var item in _playerButtons)
        {
            item.Left.SetActive(false);
            item.Right.SetActive(false);
        }
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            CharacterNames[i].text = _charData.Characters[SelectedCharacters[i].ModelIndex].CharacterName;
            CharacterSkinNames[i].text = _charData.Characters[SelectedCharacters[i].ModelIndex].Skins[SelectedCharacters[i].SkinIndex].Name;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            foreach (var item in SelectedCharacters)
            {
                stream.SendNext(item.ModelIndex);
                stream.SendNext(item.SkinIndex);
            }
        }
        else {
            foreach (var item in SelectedCharacters)
            {
                item.ModelIndex = (int)stream.ReceiveNext();
                item.SkinIndex = (int)stream.ReceiveNext();
            }
        }
    }

    public void EnableButton()
    {
        foreach (var item in _playerButtons)
        {
            item.Left.SetActive(true);
            item.Right.SetActive(true);
        }
    }

    public void EnableButton(int index, bool disableOthers = true)
    {
        if (disableOthers)
        {
            foreach (var item in _playerButtons)
            {
                item.Left.SetActive(false);
                item.Right.SetActive(false);
            }
        }

        _playerButtons[index].Left.SetActive(true);
        _playerButtons[index].Right.SetActive(true);
        _playerButtons[index + 4].Left.SetActive(true);
        _playerButtons[index + 4].Right.SetActive(true);
    }

    public void DisableButton(int index)
    {
        _playerButtons[index].Left.SetActive(false);
        _playerButtons[index].Right.SetActive(false);
        _playerButtons[index + 4].Left.SetActive(false);
        _playerButtons[index + 4].Right.SetActive(false);
    }

    public void Iterate(IterateType iType, ModelSkin type, int index)
    {
        IterateIndex del;
        if (iType == IterateType.Next)
            del = NextIndex;
        else
            del = PreviousIndex;

        switch (type)
        {
            case ModelSkin.Model:
                del(ref SelectedCharacters[index].ModelIndex, _charData.Characters.Count);
                SelectedCharacters[index].SkinIndex = 0;
                break;
            case ModelSkin.Skin:
                del(ref SelectedCharacters[index].SkinIndex, _charData.Characters[SelectedCharacters[index].ModelIndex].Skins.Count);
                break;
        }
    }

    public delegate void IterateSkin(IterateType iType, ModelSkin mType, int index);
    private delegate void IterateIndex(ref int index, int arrayLength);

    void NextIndex(ref int index, int arrayLength)
    {
        index++;
        if (index >= arrayLength)
            index = 0;
    }

    void PreviousIndex(ref int index, int arrayLength)
    {
        index--;
        if (index < 0)
            index = arrayLength - 1;
    }

    public void Test(int i)
    {
        
    }

    /// <summary>
    /// Only meant for buttons
    /// </summary>
    public void NextChar(int index)
    {
        if (IterateSkinCallback != null)
            IterateSkinCallback(IterateType.Next, ModelSkin.Model, index);
    }

    /// <summary>
    /// Only meant for buttons
    /// </summary>
    public void PrevChar(int index)
    {
        if (IterateSkinCallback != null)
            IterateSkinCallback(IterateType.Previous, ModelSkin.Model, index);
    }

    /// <summary>
    /// Only meant for buttons
    /// </summary>
    public void NextSkin(int index)
    {
        if (IterateSkinCallback != null)
            IterateSkinCallback(IterateType.Next, ModelSkin.Skin, index);
    }

    /// <summary>
    /// Only meant for buttons
    /// </summary>
    public void PrevSkin(int index)
    {
        if (IterateSkinCallback != null)
            IterateSkinCallback(IterateType.Previous, ModelSkin.Skin, index);
    }
}

public enum IterateType
{
    Next,
    Previous
}

public enum ModelSkin
{
    Model,
    Skin
}

