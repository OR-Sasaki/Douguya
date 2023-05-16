using System.Collections.Generic;
using UnityEngine;

namespace CoreData.Master
{
    public record MasterData
    {
        public static MasterData I
        {
            get
            {
                if (Instance == null)
                {
                    Debug.LogError("マスターデータがありません。");
                }
                
                return Instance;
            }
        }

        static MasterData Instance;
        
        public MasterData()
        {
            Instance = this;
        }
        
        public Dictionary<int, Item> Items;
        public Dictionary<int, Seed> Seeds;
    }
}