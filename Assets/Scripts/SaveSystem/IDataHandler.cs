public interface IDataHandler
{
    void Save(GameData data);
    void Load(GameData data, bool isNewGame = false);
}