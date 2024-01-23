using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerBasicLogic : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private GameObject playerCitadelUIMenu;
    [SerializeField] private GameObject[] garrisonedUnitsHolders;
    [SerializeField] private GameObject[] selectedUnitsHolders;

    [SerializeField] private Sprite spearmanImage;
    [SerializeField] private Sprite archerImage;
    [SerializeField] private Sprite horsemanImage;

    private RaycastHit hit;

    private ArrayList[] army = new ArrayList[2] { new ArrayList(), new ArrayList()};

    //Types of units in holder
    private Dictionary<string, int> typesOfUnitsInHolder = new Dictionary<string, int>();

    private List<string> unitsInHolder = new List<string>();

    //Types of units in selected
    private List<string> unitsInSelected = new List<string>();

    //Stores the amount of each army type
    //0 - spearman, 1 - archer, 2 - horseman
    private int[] armyTypeCount = new int[3];
    private int[] selectedTypeCount = new int[3];

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            if (Physics.Raycast(ray, out hit) && !isOverUI)
            {
                Transform hitObject = hit.transform;

                if(hitObject.CompareTag("Player"))
                {
                    SelectPlayerObject(hitObject.name);
                }
                else
                {
                    DeselectPlayerObject();
                }
            }
        }
    }
    
    private void SelectPlayerObject(string name)
    {
        switch(name)
        {
            case "PlayerCitadel":
                playerCitadelUIMenu.SetActive(true);
                break;
        }
    }
    private void DeselectPlayerObject()
    {
        playerCitadelUIMenu.SetActive(false);
    }

    public void SpawnUnitInArmy(string name, string garrisonName)
    {
        switch(name)
        {
            case "spearman":
                Spearman spearman = new Spearman();
                army[0].Add(garrisonName);
                army[1].Add(spearman);
                break;
            case "archer":
                Archer archer = new Archer();
                army[0].Add(garrisonName);
                army[1].Add(archer);
                break;
            case "horseman":
                Horseman horseman = new Horseman();
                army[0].Add(garrisonName);
                army[1].Add(horseman);
                break;
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        int typesInHolder = CountTypesInHolder();
        int typesInArmy = CountTypesInArmy();
        FillUnitsInHolder();
        if(typesInArmy > typesInHolder)
        {
            garrisonedUnitsHolders[typesInArmy - 1].SetActive(true);
            switch(unitsInHolder[typesInArmy-1])
            {
                case "spearman":
                    garrisonedUnitsHolders[typesInArmy - 1].transform.GetChild(0).GetComponent<Image>().sprite = spearmanImage;
                    garrisonedUnitsHolders[typesInArmy - 1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "1";
                    break;
                case "archer":
                    garrisonedUnitsHolders[typesInArmy - 1].transform.GetChild(0).GetComponent<Image>().sprite = archerImage;
                    garrisonedUnitsHolders[typesInArmy - 1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "1";
                    break;
                case "horseman":
                    garrisonedUnitsHolders[typesInArmy - 1].transform.GetChild(0).GetComponent<Image>().sprite = horsemanImage;
                    garrisonedUnitsHolders[typesInArmy - 1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "1";
                    break;
            }
        }
        if(typesInArmy == typesInHolder)
        {
            UpdateArmyTypeCount();
            for (int i = 0; i < 3; i++) 
            {
                garrisonedUnitsHolders[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = armyTypeCount[i].ToString();
            }
        }
    }
    //Returns how many holders are active (how many types of units is in stored in holders)
    private int CountTypesInHolder()
    {
        int types = 0;
        foreach(GameObject holder in garrisonedUnitsHolders)
        {
            if(holder.activeSelf)
            {
                types++;
            }
        }
        return types;
    }
    //Returns how many types of units are in the army
    private int CountTypesInArmy()
    {
        //List is making sure that it doesn't increment types on every unit but every new unit
        List<string> typesOfUnits = new List<string>();
        int types = 0;
        foreach(Unit unit in army[1])
        {
            if (unit.Name == "spearman" && !typesOfUnits.Contains("spearman"))
            {
                types++;
                typesOfUnits.Add("spearman");
            }
            if (unit.Name == "archer" && !typesOfUnits.Contains("archer"))
            {
                types++;
                typesOfUnits.Add("archer");
            }
            if (unit.Name == "horseman" && !typesOfUnits.Contains("horseman"))
            {
                types++;
                typesOfUnits.Add("horseman");
            }
        }
        return types;
    }
    //Stores unit types (names of units) in a variable used to fill the holder with the appropriate type of unit
    private void FillUnitsInHolder()
    {
        foreach(Unit unit in army[1])
        {
            if(!unitsInHolder.Contains(unit.Name))
            {
                unitsInHolder.Add(unit.Name);
            }
        }
    }
    //Updates array of integers of how many units are of each type in the army
    private void UpdateArmyTypeCount()
    {
        armyTypeCount = new int[3];
        foreach(Unit unit in army[1])
        {
            switch(unit.Name)
            {
                case "spearman":
                    armyTypeCount[0]++;
                    break;
                case "archer":
                    armyTypeCount[1]++;
                    break;
                case "horseman":
                    armyTypeCount[2]++;
                    break;
            }
        }
    }
    //Called from SelectingUnits to move unit from garrisoned units to selected units
    public void SelectUnit(int holderIndex)
    {
        int typesInSelected = CountTypesInSelected();
        int j = 0;

        if (!unitsInSelected.Contains(unitsInHolder[holderIndex]))
        {
            unitsInSelected.Add(unitsInHolder[holderIndex]);
        }

        foreach (string unit in unitsInSelected)
        {
            for (int i = 0; i < unitsInHolder.Count; i++)
            {
                if (unit == unitsInHolder[i])
                {
                    selectedUnitsHolders[holderIndex].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = selectedTypeCount[j].ToString();
                    return;
                }
            }
            j++;
        }

        AddNewUnitInSelected(typesInSelected);
    }
    private void AddNewUnitInSelected(int index)
    {
        selectedUnitsHolders[index].SetActive(true);

        switch(unitsInSelected[index])
        {
            case "spearman":
                selectedUnitsHolders[index].transform.GetChild(0).GetComponent<Image>().sprite = spearmanImage;
                selectedUnitsHolders[index].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "1";
                break;
            case "archer":
                selectedUnitsHolders[index].transform.GetChild(0).GetComponent<Image>().sprite = archerImage;
                selectedUnitsHolders[index].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "1";
                break;
            case "horseman":
                selectedUnitsHolders[index].transform.GetChild(0).GetComponent<Image>().sprite = horsemanImage;
                selectedUnitsHolders[index].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "1";
                break;
        }
    }
    private int CountTypesInSelected()
    {
        int types = 0;
        foreach (GameObject selected in selectedUnitsHolders)
        {
            if (selected.activeSelf)
            {
                types++;
            }
        }
        return types;
    }
}
