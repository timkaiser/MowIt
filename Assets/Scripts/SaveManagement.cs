using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManagement : MonoBehaviour {
    //struct for saving the settings
    [System.Serializable]
    struct Settings {
        public string language;
        public bool isMuted;
        public Settings(string language, bool isMuted) {
            this.language = language;
            this.isMuted = isMuted;
        }
    }

    //struct for saving the game data
    [System.Serializable]
    struct Unlocked {
        public int[] level;
        public Unlocked(int[] level) {
            this.level = level;
        }
    }

    //saves settings
    public static void SaveSettings() {
        Settings data = new Settings(GlobalVariables.language, GlobalVariables.isMuted);
    
        string destination = Application.persistentDataPath + "/settings.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);
        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    //loads settings
    public static void LoadSettings() {
        string destination = Application.persistentDataPath + "/settings.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else {
            SaveSettings();
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        Settings data = (Settings)bf.Deserialize(file);
        file.Close();

        GlobalVariables.language = data.language;
        GlobalVariables.isMuted = data.isMuted;
    }

    //saves level progress
    public static void SaveLevels() {
        Unlocked data = new Unlocked(GlobalVariables.UnlockedLevels);

        string destination = Application.persistentDataPath + "/levels.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    //loads settings
    public static void LoadLevels() {
        string destination = Application.persistentDataPath + "/levels.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else {
            SaveLevels();
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        Unlocked data = (Unlocked)bf.Deserialize(file);
        file.Close();

        GlobalVariables.UnlockedLevels = data.level;
    }
}
