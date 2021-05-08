using UnityEngine;
using System.Collections.Generic;
using Tribot;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public GameObject PlayerPrefab;
    public List<Character> Characters = new List<Character>();

    public GameObject InstatiateCharacter(CharacterIndex index)
    {
        var modelPrefab = Characters[index.ModelIndex].ModelPrefab;
        var texture = Characters[index.ModelIndex].Skins[index.SkinIndex].Texture;

        var player = Instantiate(PlayerPrefab);
        var model = Instantiate(modelPrefab);

        model.transform.parent = player.transform;
        model.transform.localPosition = Vector3.zero;

        var animator = player.AddComponent<Animator>(model.GetComponent<Animator>());
        Destroy(model.GetComponent<Animator>());

        player.SendMessage("SetAnimator", animator);

        if (texture != null)
        {
            var mesh = model.GetComponentInChildren<SkinnedMeshRenderer>();
            mesh.material = new Material(mesh.material);
            mesh.material.mainTexture = texture;
        }

        return player;
    }

    [System.Serializable]
    public class Character
    {
        public string CharacterName;
        public GameObject ModelPrefab;
        public List<Skin> Skins;

        [System.Serializable]
        public struct Skin
        {
            public string Name;
            public Texture Texture;
        }
    }
}

public class CharacterIndex
{
    public int ModelIndex;
    public int SkinIndex;
}

