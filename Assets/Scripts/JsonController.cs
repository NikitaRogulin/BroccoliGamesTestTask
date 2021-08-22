using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// может быть не моно!
public class JsonController : MonoBehaviour
{
    public PartMap[] Map { get; private set; }
    [SerializeField] private TextAsset textJson;

    public void LoadField()
    {
        Map = JsonUtility.FromJson<JSON>(textJson.text).List;
    }

    void Awake()
    {
        LoadField();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
