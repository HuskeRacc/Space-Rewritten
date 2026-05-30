using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

    public class SavingLoading : MonoBehaviour
    {
    private string SavePath => $"{Application.persistentDataPath}/savedata.txt";

    public static SavingLoading instance;

    private void Awake()
    {
        instance = this;
    }

    [ContextMenu("Save")]
    public void OnClick_Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    [ContextMenu("Load")]
    public void OnClick_Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }

    private void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    

    private Dictionary<string,object> LoadFile()
    {
        if(!File.Exists(SavePath))
        {
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void CaptureState(Dictionary<string,object> state)
    {
        foreach (var saveable in Object.FindObjectsByType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string,object> state)
    {
        foreach (var saveable in Object.FindObjectsByType<SaveableEntity>())
        {
            if(state.TryGetValue(saveable.Id,out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }    
}
