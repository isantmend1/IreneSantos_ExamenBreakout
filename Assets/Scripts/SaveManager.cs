using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using Unity.VisualScripting;

public static class SaveManager
{
    private static string path = Application.persistentDataPath + "/save.dat";
    private static int record = 0;
    // private static int[] records = new int[5];

    public static void SaveRecord(int pooints)
    {
      //  for(int i = 0; i < records.Length; i++)
        //{
            if (pooints > record)
            {
                record = pooints;
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Create);

                RecordData data = new RecordData(record);

                formatter.Serialize(stream, data);
                stream.Close();
          //  }
        }
        
    }

   public static int LoadRecord()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RecordData data = formatter.Deserialize(stream) as RecordData;  // equivalent to (RecordData)formatter.Deserialize(stream);
            stream.Close();

            record = data.GetRecord();
        } else
        {
            record = 0;
        }
        return record;
    }
}
