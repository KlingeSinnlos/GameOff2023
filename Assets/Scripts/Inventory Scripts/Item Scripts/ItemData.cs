using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
	public string id;
	public string displayName;
	public string description;
	public float weight;
	public bool weightUnlocked;
	public Sprite icon;
	public GameObject prefab;
	public Vector2 sizeOnScale;
	public Quality quality;
	
	public enum Quality
	{
		Great,
		Good,
		Bad
	}
}
