using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager _instance;
    public static CheckpointManager Instance{get {return _instance;}}

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Checkpoint defaultCheckpoint;

    private Checkpoint.Point _checkpoint;
    public Checkpoint.Point CurrentCheckpoint
    {
        get
        {
            return _checkpoint;
        }
        set 
        {
                _checkpoint = value;
                Save();
        }
    }

    public UnityEvent OnLoad;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Load();
    }

    public void Load()
    {
        if(File.Exists($"{Application.persistentDataPath}/SaveData.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open($"{Application.persistentDataPath}/SaveData.dat", FileMode.Open);

            _checkpoint = (Checkpoint.Point)formatter.Deserialize(file);

            file.Close();
        }
        else
        {
            // set the default checkpoint
            _checkpoint = defaultCheckpoint.point;
        }

        // Set the player position
        player.transform.position = Vector3.right * (_checkpoint.position);

        OnLoad.Invoke();
    }

    private void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create($"{Application.persistentDataPath}/SaveData.dat");
        formatter.Serialize(file, _checkpoint);
        file.Close();
    }
}