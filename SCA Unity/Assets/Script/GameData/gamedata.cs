using UnityEngine;
using System.Collections.Generic; 

[CreateAssetMenu(fileName = "GameData", menuName = "Game/GameData")]
public class GameData : ScriptableObject
{
    public bool ±ÔÄ¢¾î±è;

   
    public List<string> itemList = new List<string>(); 

    public void AddItem(string item)
    {
        itemList.Add(item);
    }

    public void UseItem(string item)
    {
        itemList.Remove(item);
    }
}