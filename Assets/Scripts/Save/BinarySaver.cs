using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using HappyPassengers.Scripts.Save;
using UnityEngine;

namespace HappyPassengers.Scripts
{
    interface ISaver
    {
        void Save<T>(T data) where T : ISavedData;
        T Load<T>() where T : ISavedData;
    }

    class BinarySaver: ISaver
    {
        //"C:/Users/username/AppData/LocalLow/DefaultCompany/HappyPassenger/T.cache"


        public void Save<T>(T saveData) where T: ISavedData
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var fs = new FileStream(Application.persistentDataPath + "/"+ saveData.GetType().Name + ".cache", FileMode.Create))
            {
                bf.Serialize(fs, saveData);
            }
        }

        public T Load<T>() where T : ISavedData
        {
            BinaryFormatter bf = new BinaryFormatter();
            string filepath = Application.persistentDataPath + "/" + typeof(T).Name + ".cache";
            if (File.Exists(filepath))
            {
                using (var fs = new FileStream(filepath, FileMode.Open))
                {
                    return (T) bf.Deserialize(fs);
                }
            }
            return default(T);
        }
    }
}
