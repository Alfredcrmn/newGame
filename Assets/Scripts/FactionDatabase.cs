using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu]
public class FactionDatabase : ScriptableObject
{
    public Faction[] faction;
    public int FactionCount
    {
        get
        {
            return faction.Length;
        }
    }

    public Faction GetFaction(int index)
    {
        return faction[index];
    }
}
