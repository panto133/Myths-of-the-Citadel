using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI amount1;
    [SerializeField] private TextMeshProUGUI amount2;
    [SerializeField] private Image costImage1;
    [SerializeField] private Image costImage2;

    [SerializeField] private Sprite foodSprite;
    [SerializeField] private Sprite woodSprite;
    [SerializeField] private Sprite goldSprite;
    [SerializeField] private Sprite bookSprite;

    [SerializeField] private GameObject gameLogic;

    private int spearmanCost1 = 60;
    private int spearmanCost2 = 10;

    private int archerCost1 = 45;
    private int archerCost2 = 15;

    private int horsemanCost1 = 75;
    private int horsemanCost2 = 15;

    //Fetching all of the cost so that hover doesn't lag when trying to access the script
    private void Start()
    {
        spearmanCost1 = UniversalData.spearmanCost1;
        spearmanCost2 = UniversalData.spearmanCost2;

        archerCost1 = UniversalData.archerCost1;
        archerCost2 = UniversalData.archerCost2;

        horsemanCost1 = UniversalData.horsemanCost1;
        horsemanCost2 = UniversalData.horsemanCost2;
    }
    //Assigning information about unit cost on hover
    public void HoverInformationEnter(string buttonName)
    {
        costImage1.enabled = true;
        costImage2.enabled = true;
        switch(buttonName)
        {
            case "spearman":
                costImage1.sprite = foodSprite;
                amount1.text = spearmanCost1.ToString();
                costImage2.sprite = woodSprite;
                amount2.text = spearmanCost2.ToString();
                break;
            case "archer":
                costImage1.sprite = woodSprite;
                amount1.text = archerCost1.ToString();
                costImage2.sprite = goldSprite;
                amount2.text = archerCost2.ToString();
                break;
            case "horseman":
                costImage1.sprite = foodSprite;
                amount1.text = horsemanCost1.ToString();
                costImage2.sprite = goldSprite;
                amount2.text = horsemanCost2.ToString();
                break;
        }
    }
    public void HoverInformationExit()
    {
        costImage1.enabled = false;
        amount1.text = null;
        costImage2.enabled = false;
        amount2.text = null;
    }

    public void TrainUnit(string unitName)
    {
        gameLogic.GetComponent<TrainingUnits>().StartTraining(unitName, "PlayerCitadel");
    }
}
