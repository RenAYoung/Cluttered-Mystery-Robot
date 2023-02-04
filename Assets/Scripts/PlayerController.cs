using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	private Rigidbody body;
	public float Speed = 500;
    public float Spin = 1;
	private Animator animator;
	public GameObject quitButton;
	public GameObject continueButton;
	public GameObject introCanvas;
	
	// pieces for interactions with objects
	public string itemHeld = "";
	public string itemCategory = "";
	public GameObject pickupButton;
	public TextMeshProUGUI itemHeldInfo;
	private Collider tempCollider;
	public GameObject sortButton; 
	private string currObj = "";

	// interactions with story objects
	public GameObject investigateButton;
	public GameObject clipboardCanvas;
	public GameObject keyCanvas;
	public GameObject laptopCanvas;
	public GameObject radioCanvas;
	public GameObject chestCanvas;
	public GameObject crystalCanvas;
	public AudioSource radioClip;
	private bool keyHeld = false;

	void Awake()
	{
		body = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();

		pickupButton.SetActive(false);
		itemHeldInfo.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -1f);	
		sortButton.SetActive(false);
		investigateButton.SetActive(false);
		clipboardCanvas.SetActive(false);
		keyCanvas.SetActive(false);
		laptopCanvas.SetActive(false);
		radioCanvas.SetActive(false);
		chestCanvas.SetActive(false);
		crystalCanvas.SetActive(false);
		introCanvas.SetActive(true);

	}

	private void FixedUpdate()
    {
		
        float direction = Input.GetAxis("Vertical") * Speed;
        float rotation = Input.GetAxis("Horizontal") * Spin;
        body.AddRelativeForce(new Vector3(0, 0, direction));
		animator.SetBool("Walking", direction != 0);
        body.AddTorque(new Vector3(0, rotation, 0));
		
		
    }

	void OnTriggerEnter(Collider other) 
	{
		// check not holding an item and collider is not a wastecan
		Debug.Log(other.gameObject.tag);
		if (itemHeld.Length == 0 && other.gameObject.tag != "WasteCan" && other.gameObject.tag != "Story") {
			pickupButton.SetActive(true);
		}
		if (itemHeld.Length != 0 && other.gameObject.tag == "WasteCan") {
			sortButton.SetActive(true);
		}
		if (other.gameObject.tag == "Story") {
			if(other.gameObject.name != "Chest" || keyHeld)
				investigateButton.SetActive(true);
		}
		tempCollider = other;
	}

	void OnTriggerExit(Collider other)
	{
		pickupButton.SetActive(false);
		sortButton.SetActive(false);
		investigateButton.SetActive(false);
	}

	public void clickPickUp()
	{
		// add item name to itemHeld
		itemHeld = tempCollider.gameObject.name;
		// display itemHeld info
		itemHeldInfo.text = "Current Item: " + itemHeld;
		itemHeldInfo.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0);	
		// destroy collided object
		itemCategory = tempCollider.gameObject.tag;
		Destroy(tempCollider.gameObject);
		// disable button
		pickupButton.SetActive(false);
	}

	public void clickSort()
	{
		string canChoice = tempCollider.gameObject.name.Split(' ')[0];
		//check for correct wastecan
		if(itemCategory == canChoice) 
		{
			// empty item name
			itemHeld = "";
			itemHeldInfo.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, -1f);
			//disable button
			sortButton.SetActive(false);
		}
		else
		{

		}
	}

	public void investigate()
	{
		currObj = tempCollider.gameObject.name;
		// check for clipboard
		if(currObj == "Clipboard") 
			clipboardCanvas.SetActive(true);
		// check for key
		if(currObj == "Key")
		{
			keyCanvas.SetActive(true);
			keyHeld = true;
		}
		// check for laptop
		if(currObj == "Laptop")
			laptopCanvas.SetActive(true);
		// check for radio
		if(currObj == "Radio")
		{
			radioCanvas.SetActive(true);
			radioClip.Play();
		}
		// check for chest
		if(currObj == "Chest")
			chestCanvas.SetActive(true);
		// check for crystal
		if(currObj == "Crystal")
			crystalCanvas.SetActive(true);
		Destroy(tempCollider.gameObject);
	}

	public void clickReturn()
	{
		clipboardCanvas.SetActive(false);
		keyCanvas.SetActive(false);
		laptopCanvas.SetActive(false);
		radioCanvas.SetActive(false);
		chestCanvas.SetActive(false);
		crystalCanvas.SetActive(false);
		investigateButton.SetActive(false);
	}

	public void clickQuit()
	{
		Application.Quit();
	}

	public void clickContinue()
	{
		introCanvas.SetActive(false);
	}
}
