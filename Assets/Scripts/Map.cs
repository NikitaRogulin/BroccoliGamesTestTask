using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private JsonController jsonController;
    [SerializeField] private GameObject partMapPrefab;

    private void Start()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        foreach (PartMap e in jsonController.Map)
        {
            var currentID = e.Id;
            var posE = new Vector2(e.X, e.Y);
            var currentSprite = Resources.Load<Sprite>("Sprites/" + currentID);
            var instPartMap = Instantiate(partMapPrefab, posE, Quaternion.identity);
            instPartMap.GetComponent<SpriteRenderer>().sprite = currentSprite;
        }
    }
}
