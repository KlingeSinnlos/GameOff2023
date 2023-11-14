using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
	[SerializeField] private GameObject itemIcon;
	[SerializeField] private InventoryManager inventoryManager;

	private GameObject realUI;
	
	private readonly Dictionary<Image, ItemData> itemIconRendererDictionary = new();
	private GameObject scrollerParent;
	private TMP_Text nameText;
	private TMP_Text descriptionText;
	private TMP_Text weightText;
	
	private void Awake()
	{
		var realUITransform = transform.GetChild(0);
		realUI = realUITransform.gameObject;
		
		scrollerParent = realUITransform.Find("Icon Scroller").gameObject;
		nameText = realUITransform.Find("Name").GetComponent<TMP_Text>();
		descriptionText = realUITransform.Find("Description").GetComponent<TMP_Text>();
		weightText = realUITransform.Find("Weight Number").GetComponent<TMP_Text>();
		
		realUI.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var currentSelectedObject = EventSystem.current.currentSelectedGameObject;
			if (currentSelectedObject) return;
			print(currentSelectedObject);
		}
		
		if (!Input.GetKeyDown(KeyCode.E)) return;
		if (!isShowingInventory) ShowInventory();
		else HideInventory();
	}

	private bool isShowingInventory;
	private void ShowInventory()
	{
		isShowingInventory = true;
		realUI.SetActive(true);
		
		print("opened Inventory");
		foreach (var itemData in inventoryManager.inventory)
		{
			var currentIcon = Instantiate(itemIcon).GetComponent<Image>();
			currentIcon.sprite = itemData.icon;
			currentIcon.gameObject.GetComponent<Button>().onClick.AddListener(delegate
			{
				SelectItem(currentIcon.gameObject, itemData);
			});
			itemIconRendererDictionary.Add(currentIcon, itemData);
		}
		CreateIconScroller();
	}

	private void CreateIconScroller()
	{
		var renderedIconCount = 0;
		foreach (var iconRendererTransform in itemIconRendererDictionary.Select(iconRenderer => iconRenderer.Key.GetComponent<RectTransform>()))
		{
			iconRendererTransform.SetParent(scrollerParent.transform);
			iconRendererTransform.localPosition = new Vector2(renderedIconCount * 75 - 150, 0);
			iconRendererTransform.localScale = new Vector3(0.5f, 0.5f, 1);
			
			renderedIconCount ++;
		}
	}

	private void HideInventory()
	{
		isShowingInventory = false;
		realUI.SetActive(false);
		
		itemIconRendererDictionary.Clear();
	}

	public void SelectItem(GameObject iconRenderer, ItemData itemData)
	{
		var iconRendererTransform = iconRenderer.GetComponent<RectTransform>();
		
		var scrollerParentLocalPosition = scrollerParent.GetComponent<RectTransform>().localPosition;
		scrollerParentLocalPosition = new Vector3(-iconRendererTransform.localPosition.x,
			scrollerParentLocalPosition.y, scrollerParentLocalPosition.z);
		scrollerParent.GetComponent<RectTransform>().localPosition = scrollerParentLocalPosition;
		
		iconRendererTransform.localScale = new Vector3(1, 1, 1);
		nameText.text = itemData.displayName;
		descriptionText.text = itemData.description;
		weightText.text = itemData.weight.ToString(CultureInfo.CurrentCulture);
	}
}
