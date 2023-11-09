﻿
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        public bool isSound = true;
        public int bestScore;
        public int currentStage;
        public int visualStage;
        public int oranges;
        public int defaultCircleId;
        public int bossCircleId;
        
        public KnifeType knifeType = KnifeType.DEFAULT;
        public List<KnifeType> purchasedKnives = new List<KnifeType>();
        
        public bool IsKnifePurchased(KnifeType type)
        {
            if (type == KnifeType.DEFAULT) return true;
            return purchasedKnives.Contains(type);
        }
    
        public void Purchase(KnifeType type)
        {
            if (purchasedKnives.Contains(type)) return;
            purchasedKnives.Add(type);
        }


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
