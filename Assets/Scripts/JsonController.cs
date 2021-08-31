using UnityEngine;

public class JsonController : MonoBehaviour
{
    [SerializeField] private TextAsset textJson;

    public PartMap[] Map { get; private set; }

    private void Awake()
    {
        LoadField();
    }

    private void LoadField()
    {
        Map = JsonUtility.FromJson<JSON>(textJson.text).List;
    }
}
