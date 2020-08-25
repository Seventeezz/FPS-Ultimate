using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
	[Header("Pick Up Item Tool Tip Parameters")]
	public string itemName;
	public string itemType;
	public enum ItemRarityEnum
	{
		Common, Uncommon, Rare, Epic, Legendary, Mythic
	}
	public ItemRarityEnum itemRarity;
	public Color common;
	public Color uncommon;
	public Color rare;
	public Color epic;
	public Color legendary;
	public Color mythic;
	public string itemAmount;
	public string pickUpButtonText;

	[Header("Setup Parameters")]
	public GameObject toolTipWidget;
	public RawImage itemBackground;

	public UnityAction onPickup;
	
	
	private TextMeshProUGUI[] tmpTexts;
	private TextMeshProUGUI pickUpButton;
	private TextMeshProUGUI tmpItemType;
	private TextMeshProUGUI tmpItemName;
	private TextMeshProUGUI tmpItemRarity;
	private TextMeshProUGUI tmpItemAmount;
	private string pickUpButtonTextLowerCase;
	private bool isInRange = false;
	private bool canPickup = true;
	private ItemWUI itemWUI;

	
	// Use this for initialization
	void Start ()
	{
		tmpTexts = gameObject.GetComponentsInChildren<TextMeshProUGUI> ();
		pickUpButtonTextLowerCase = pickUpButtonText.ToLower();
		itemWUI = GetComponentInChildren<ItemWUI>();
		if (itemWUI == null)
		{
			Debug.LogError("item WUI script missing in child!");
		}
		
		for (int i = 0; i < tmpTexts.Length; i++)
		{
			switch (tmpTexts [i].name)
			{
			case "_PickUpBtnText":
				pickUpButton = tmpTexts [i];
				pickUpButton.text = pickUpButtonText;
				break;

			case "_txtType":
				tmpItemType = tmpTexts [i];
				tmpItemType.text = itemType;
				break;

			case "_txtItemName":
				tmpItemName = tmpTexts [i];
				tmpItemName.text = itemName;
				break;

			case "_txtRarity":
				tmpItemRarity = tmpTexts [i];
				tmpItemRarity.text = itemRarity.ToString ();
				break;

			case "_txtAmount":
				pickUpButton = tmpTexts [i];
				pickUpButton.text = itemAmount;
				break;
			}
		}

		switch (tmpItemRarity.text)
		{
		case "Common":
			itemBackground.color = common;
			break;
		
		case "Uncommon":
			itemBackground.color = uncommon;
			break;
		
		case "Rare":
			itemBackground.color = rare;
			break;
		
		case "Epic":
			itemBackground.color = epic;
			break;
		
		case "Legendary":
			itemBackground.color = legendary;
			break;
		
		case "Mythic":
			itemBackground.color = mythic;
			break;
		}
		
		toolTipWidget.SetActive(false);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.transform.CompareTag("Player"))
		{
			toolTipWidget.SetActive (true);
			isInRange = true;
			
		}
	}


	private void Update()
	{
		if (!isInRange)
			return;
		itemWUI.LookAtCamera();
		if (Input.GetKeyDown(pickUpButtonTextLowerCase) && canPickup)
		{
			onPickup.Invoke();
			canPickup = false;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.transform.CompareTag("Player"))
		{
			toolTipWidget.SetActive (false);
			isInRange = false;
		}
	}
	
	
}
