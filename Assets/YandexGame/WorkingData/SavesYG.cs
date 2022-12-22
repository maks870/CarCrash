
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;

        // Ваши сохранения
        public int money = 1;
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];


        public int coins = 0;
        public int gems = 0;
        public List<string> collectedItems = new List<string>() { "Car1", "Char1", "Color1" };
        public string currentCharacterItem;
        public string currentCarColorItem;
        public string currentCarModelItem;

        public int mediumGemAdViewed = 0;
    }
}
