namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
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

        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }
    }
}
