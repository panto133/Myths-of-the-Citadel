//Holding the data about units
public static class UniversalData
{
    public static int spearmanCost1 = 60;
    public static int spearmanCost2 = 10;
    public static float spearmanTrainTime = 2f;

    public static int archerCost1 = 45;
    public static int archerCost2 = 15;
    public static float archerTrainTime = 2f;

    public static int horsemanCost1 = 75;
    public static int horsemanCost2 = 15;
    public static float horsemanTrainTime = 3f;

    public enum TypesOfUnits
    {
        spearman,
        archer,
        horseman
    }
}
