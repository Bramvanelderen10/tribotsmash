using Tribot;
using UnityEngine;

public class PickUpAbility : PickUp
{
    public AbilityTypes Ability;
    private Ability _abilityPrefab;

    protected override void Start()
    {
        base.Start();

        if (!Sprite)
            return;

        var data = Resources.Load<AbilityData>("AbilityData");
        _abilityPrefab = data.GetAbilityPrefab(Ability).GetComponent<Ability>();
        Sprite.sprite = _abilityPrefab.UiIcon;
    }

    protected override void ExtendedExecute(IPlayerManager player)
    {
        player.AddAbility(Ability);
        player.PopUp("New Ability ", _abilityPrefab.MappedButton.ToString() + ": " + _abilityPrefab.AbilityName + "!");
        Destroy(gameObject);
    }
}

