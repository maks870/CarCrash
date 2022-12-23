using System.Collections.Generic;

[System.Serializable]
public class PlayerWrapper
{
    public List<string> collectibles = new List<string>();
    public string currentCharacterItem = "";
    public string currentCarColorItem = "";
    public string currentCarModelItem = "";

    //public PlayerWrapper(List<string> collectibles, string currentCharacterItem, string currentCarColorItem, string currentCarModelItem)
    //{
    //    this.collectibles = collectibles;
    //    this.currentCharacterItem = currentCharacterItem;
    //    this.currentCarColorItem = currentCarColorItem;
    //    this.currentCarModelItem = currentCarModelItem;
    //}
}

public class CustomString
{
    string value;
}
