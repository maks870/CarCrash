
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public bool isFirstSession2 = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;

        // Ваши сохранения
        public int money = 1;
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];


        public int coins = 100;
        public int gems = 0;

        public PlayerWrapper playerWrapper = new PlayerWrapper();

        public int mediumGemAdViewed = 0;
    }
}
