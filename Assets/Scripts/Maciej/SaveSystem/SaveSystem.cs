using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveMoney (MoneySO moneySO, EndOfDayStatsSO statsSO)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.remembrance";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveVariables data = new SaveVariables(moneySO, statsSO);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveVariables LoadVariables()
    {
        string path = Application.persistentDataPath + "/player.remembrance";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveVariables data = formatter.Deserialize(stream) as SaveVariables;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
