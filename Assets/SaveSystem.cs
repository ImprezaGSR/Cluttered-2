using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // public static void SavePlayer(HealthScript healthScript, AudioManager audioManager, ShipInventory shipInventory){
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     string path = Application.persistentDataPath + "/player.clu";
    //     FileStream stream = new FileStream(path, FileMode.Create);

    //     PlayerStatus data = new PlayerStatus(healthScript, audioManager, shipInventory);

    //     formatter.Serialize(stream, data);
    //     stream.Close();
    // }

    // public static PlayerStatus LoadPlayer(){
    //     string path = Application.persistentDataPath + "/player.clu";
    //     if (File.Exists(path)){
    //         BinaryFormatter formatter = new BinaryFormatter();
    //         FileStream stream = new FileStream(path, FileMode.Open);

    //         PlayerStatus data = formatter.Deserialize(stream) as PlayerStatus;
    //         stream.Close();

    //         return data;
    //     }else{
    //         Debug.LogError("Save file not found in " + path);
    //         return null;
    //     }
    // }
}
