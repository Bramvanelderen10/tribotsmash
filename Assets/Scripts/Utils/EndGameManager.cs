using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    public Button BackEndOfRound;

    public GameObject PanelLeft, PanelRight;
    public GameObject TextLeftPrefab, TextRightPrefab;


	// Use this for initialization
	void Start ()
	{
	    EventScript.Instance.SetSelectedGameObject(BackEndOfRound.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddPlayerScores(Dictionary<int, int> playerScores)
    {
        foreach (var score in playerScores)
        {
            var leftObj = Instantiate(TextLeftPrefab);
            leftObj.transform.parent = PanelLeft.transform;
            var leftText = leftObj.GetComponent<TextMeshProUGUI>();
            leftText.text = "Player " + score.Key + ":";

            var rightObj = Instantiate(TextRightPrefab);
            rightObj.transform.parent = PanelRight.transform;
            var rightText = rightObj.GetComponent<TextMeshProUGUI>();
            rightText.text = score.Value.ToString();
        }
    }
}
