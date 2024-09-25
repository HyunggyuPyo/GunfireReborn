using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "SO/Add Item")]
public class ItemSO : ScriptableObject
{
    public GameObject itemPrefab;
    public int id;
    public string Name;
    public string toolTip;
    public Sprite image;
}
