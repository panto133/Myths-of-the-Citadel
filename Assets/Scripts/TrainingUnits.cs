using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingUnits : MonoBehaviour
{
    [SerializeField] private Sprite spearmanImage;
    [SerializeField] private Sprite archerImage;
    [SerializeField] private Sprite horsemanImage;

    [SerializeField] private GameObject[] trainUnitHolder;
    private GameObject currentlyFreeUnitHolder;

    List<string> trainingOrder = new List<string>();
    List<string> nameTrainingOrder = new List<string>();

    private float spearmanTrainingTime;
    private float archerTrainingTime;
    private float horsemanTrainingTime;

    private float timer;
    private float timeCap;
    private bool training = false;

    private Image blackMask;

    //Fetch data about units to save time during runtime
    private void Start()
    {
        spearmanTrainingTime = UniversalData.spearmanTrainTime;
        archerTrainingTime = UniversalData.archerTrainTime;
        horsemanTrainingTime = UniversalData.horsemanTrainTime;
    }
    private void Update()
    {
        if(training)
        {
            //Cooldown effect
            timer -= Time.deltaTime;
            blackMask.fillAmount = timer/timeCap;

            if(timer <= 0)
            {
                //When finished training call function to wrap up and train next if list is not empty
                FinishedTraining();
                if (trainingOrder.Count != 0)
                    TrainNext(nameTrainingOrder[0]);
            }
        }
    }
    //Function called from button to start training units
    public bool StartTraining(string _unitName, string _garrisonName)
    {
        //If there is no available spot, return false to indicate that no units can't be trained
        if (!AvailableQueue(_unitName)) return false;

        //Adding unit to training order and name of the unit to be trained
        trainingOrder.Add(_unitName);
        nameTrainingOrder.Add(_unitName);
        //If there are no currently training units, start training
        if(!training)
        TrainNext(_unitName);

        return true;
    }
    private void TrainNext(string name)
    {
        //Set up image for the currently training unit
        switch (name)
        {
            case "spearman":
                currentlyFreeUnitHolder.GetComponent<Image>().sprite = spearmanImage;
                timeCap = spearmanTrainingTime;
                training = true;
                break;
            case "archer":
                currentlyFreeUnitHolder.GetComponent<Image>().sprite = archerImage;
                timeCap = archerTrainingTime;
                training = true;
                break;
            case "horseman":
                currentlyFreeUnitHolder.GetComponent<Image>().sprite = horsemanImage;
                timeCap = horsemanTrainingTime;
                training = true;
                break;

        }
        //Initialize black mask and timer to start counting down
        blackMask = currentlyFreeUnitHolder.transform.GetChild(0).GetComponent<Image>();
        blackMask.fillAmount = 1f;
        timer = timeCap;
    }
    private bool AvailableQueue(string _unitName)
    {
        //If there is no available spot, don't allow adding more units to training list
        if (trainingOrder.Count == 4) return false;

        //Enabling the UI of next holder of next unit in queue
        trainUnitHolder[trainingOrder.Count].SetActive(true);
        //If there are no training units, start training the next from the list
        if(!training) 
            currentlyFreeUnitHolder = trainUnitHolder[trainingOrder.Count];

        //Setting the image for units to be trained
        switch(_unitName)
        {
            case "spearman":
                trainUnitHolder[trainingOrder.Count].GetComponent<Image>().sprite = spearmanImage;
                break;
            case "archer":
                trainUnitHolder[trainingOrder.Count].GetComponent<Image>().sprite = archerImage;
                break;
            case "horseman":
                trainUnitHolder[trainingOrder.Count].GetComponent<Image>().sprite = horsemanImage;
                break;
        }
        return true;
    }
    //Clearing previously trained units and updating UI to match the images of next units to be trained
    private void FinishedTraining()
    {
        GetComponent<PlayerBasicLogic>().SpawnUnitInArmy(trainingOrder[0],"citadel");
        trainingOrder.RemoveAt(0);
        nameTrainingOrder.RemoveAt(0);
        trainUnitHolder[trainingOrder.Count].SetActive(false);
        training = false;

        for (int i = 0; i < trainingOrder.Count; i++) 
        {
            switch(nameTrainingOrder[i])
            {
                case "spearman":
                    trainUnitHolder[i].GetComponent<Image>().sprite = spearmanImage;
                    break;
                case "archer":
                    trainUnitHolder[i].GetComponent<Image>().sprite = archerImage;
                    break;
                case "horseman":
                    trainUnitHolder[i].GetComponent<Image>().sprite = horsemanImage;
                    break;
            }
        }
        
    }
}
