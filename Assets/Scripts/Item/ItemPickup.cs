using UnityEngine;


public class ItemPickup : MonoBehaviour
{
    public Item item;
    
    
    private ItemManager itemManager;
    private Material material;
    private Material originMaterial;

    #region Map name to ID

    private static readonly int NormalMap = Shader.PropertyToID("_NormalMap");
    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
    private static readonly int MetallicMap = Shader.PropertyToID("_MetallicMap");
    private static readonly int BumpMap = Shader.PropertyToID("_BumpMap");
    private static readonly int MetallicGlossMap = Shader.PropertyToID("_MetallicGlossMap");
    private static readonly int StartTime = Shader.PropertyToID("_StartTime");
    private static readonly int EdgeColor = Shader.PropertyToID("_EdgeColor");

    #endregion
    

    void Start()
    {
        itemManager = transform.parent.GetComponent<ItemManager>();
        itemManager.onPickup += OnPickup;
        
        originMaterial = GetComponentInChildren<Renderer>().material;
        material = new Material(Shader.Find("Shader Graphs/Dissolve"));
        Texture baseMap = originMaterial.GetTexture(BaseMap);
        Texture normalMap = originMaterial.GetTexture(BumpMap);
        Texture metallicMap = originMaterial.GetTexture(MetallicGlossMap);
        material.SetTexture(BaseMap, baseMap);
        material.SetTexture(NormalMap, normalMap);
        material.SetTexture(MetallicMap, metallicMap);
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up, .5f);
    }

    void OnPickup()
    {
        //Play Dissolve effect
        material.SetFloat(StartTime, Time.timeSinceLevelLoad);
        float intensity = 2f;
        float factor = Mathf.Pow(2, intensity);
        material.SetColor(EdgeColor, itemManager.itemBackground.color.linear * factor);
        GetComponentInChildren<Renderer>().material = material;
        
        //Add to the Inventory
        ScriptableObject.CreateInstance(typeof(Item));
        Inventory.instance.Add(item);
        
        //Destroy gameObject after picking up
        Destroy(itemManager.gameObject, 2f);
    }
    
}
