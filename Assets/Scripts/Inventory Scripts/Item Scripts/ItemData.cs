using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
	public string id;
	public string displayName;
	public Sprite icon;
	public GameObject prefab;
}
