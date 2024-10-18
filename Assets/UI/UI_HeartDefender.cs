using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeartDefender : MonoBehaviour
{
    public GameObject heartDefenderPrefab;
    public GameObject addHeartDefenderPrefab;

    private List<GameObject> heartDefenders = new List<GameObject>();

    public Transform cardContainer;

    public int addDefenderCost;

    public Button addDefenderButton;

    [SerializeField] private int maxDefenders;
    RectTransform parentRectTransform; // UI parent reference for camera shake

    void Start()
    {
        // assign the listener to the starting addHeartDefender UI element
        addDefenderButton.onClick.AddListener(BuyDefender);
        parentRectTransform = GetComponent<RectTransform>().parent.GetComponent<RectTransform>();
    }


    // if player has enough $ and hasn't exceeded the defender limit, add a new defender
    public void BuyDefender()
    {
        if (heartDefenders.Count < maxDefenders)
        {
            if (CurrencyManager.GetCurrentMoney() >= addDefenderCost)
            {
                AddDefender();
            }
            else
            {
                Debug.Log("not enough money");
                ShakeCamera();
            }
        }
        else
        {
            Debug.Log("you've reached the max limit of defenders");
        }
    }

    // add a new defender by swapping it with the addDefender placeholder and then move the buy UI element to the next slot down
    private void AddDefender()
    {
        CurrencyManager.RemoveMoney(addDefenderCost);
        GameObject currentPlaceHolder = cardContainer.GetChild(cardContainer.childCount - 1).gameObject;

        SwapWithDefender(currentPlaceHolder);

        AddNewPlaceHolder();
    }


    // swap the buy UI element with the heart defender
    private void SwapWithDefender(GameObject placeholder)
    {
        GameObject newDefender = Instantiate(heartDefenderPrefab, placeholder.transform.position, Quaternion.identity);
        newDefender.transform.SetParent(cardContainer, false);

        heartDefenders.Add(newDefender);

        Destroy(placeholder);
    }

    // add a new UI buy element in place of the old one, moved down to the end of the row
    private void AddNewPlaceHolder()
    {
        GameObject newPlaceholder = Instantiate(addHeartDefenderPrefab, cardContainer);
        AssignListener(newPlaceholder);

    }

    // assigns the click functionality for the buy UI element
    private void AssignListener(GameObject placeholder)
    {
        addDefenderButton = placeholder.GetComponent<Button>();

        addDefenderButton.onClick.AddListener(BuyDefender);
    }

    private void ShakeCamera()
    {
        CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();

        if (cameraShake != null && parentRectTransform != null)
        {
            cameraShake.StartCameraShake(.2f, .5f, parentRectTransform); // shake camera to signal unable to perform action
        }
    }








}
