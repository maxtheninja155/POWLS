using System;

[Serializable]
public class PowlSaveData
{
    public string owlID;       // Matches the PowlSO.id
    public bool hasMet;        // Have we unlocked this in the Powldex?
    public long lastVisitTicks;
    public int timesEncountered;
}