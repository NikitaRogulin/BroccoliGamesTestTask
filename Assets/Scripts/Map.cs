using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private JsonController jsonController;
    [SerializeField] private GameObject partMapPrefab;

    private Dictionary<string, Texture2D> textures;

    public Vector3 LeftTopBorder { get; private set; }
    public Vector3 RightBottomBorder { get; private set; }

    private void Awake()
    {
        WriteAndSaveTextures();
    }

    private void Start()
    {
        CreateMap();

        var firstPartMap = jsonController.Map[0];
        var lastPartMap = jsonController.Map[jsonController.Map.Length - 1];

        LeftTopBorder = new Vector3(firstPartMap.X, firstPartMap.Y + firstPartMap.Height);
        RightBottomBorder = new Vector3(lastPartMap.X + lastPartMap.Width, lastPartMap.Y);
    }

    public void CreateMap()
    {
        foreach (PartMap e in jsonController.Map)
        {
            var posE = new Vector2(e.X, e.Y);
            var currentTexture = textures[e.Id];
            var currentSprite = Sprite.Create(currentTexture, new Rect(0, 0, e.Width*100, e.Height*100), Vector2.zero);
            currentSprite.name = e.Id;
            var instPartMap = Instantiate(partMapPrefab, posE, Quaternion.identity);
            instPartMap.GetComponent<SpriteRenderer>().sprite = currentSprite;
            var collider = instPartMap.GetComponent<BoxCollider2D>();
            collider.size = new Vector2(e.Width, e.Height);
            collider.offset = new Vector2(e.Width / 2, e.Height / 2);
        }
    }

    private void WriteAndSaveTextures()
    {
        var allTextures = Resources.LoadAll<Texture2D>("Sprites");
        textures = new Dictionary<string, Texture2D>(allTextures.Length);
        foreach(var e in allTextures)
        {
            textures.Add(e.name, e);
        }
    }
}
