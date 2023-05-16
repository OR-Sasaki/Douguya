using CoreData;

namespace Core
{
    public static class Main
    {
        // アイテム管理
        // 生産場所ごとロジック
        // 売るロジック
        // 未来予測ロジック

        public static void NextDay(SaveData saveData)
        {
            saveData.Game.Days += 1;
            
            Garden.NextDay(saveData);
        }
    }
}