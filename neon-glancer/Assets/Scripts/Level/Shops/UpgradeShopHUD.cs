using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopHUD : MonoBehaviour
{
    internal static UpgradeShopHUD instance;

    [SerializeField] GameObject shopScreen;

    [SerializeField] GameObject notEnoughText;

    bool canInteract;
    public bool isOpened;

    void Awake()
    {
        instance = this;

        shopScreen.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canInteract && !shopScreen.activeSelf)
        {
            ToggleShopScreen(true, false);
            Cursor.SetCursor(null, new Vector2(16, 16), CursorMode.Auto);
        }
        else if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape)) && shopScreen.activeSelf)
        {
            ToggleShopScreen(false, true);
            Cursor.SetCursor(HUDController.instance.battleCursor, new Vector2(16, 48), CursorMode.Auto);

            if (notEnoughText.activeSelf)
            {
                notEnoughText.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleInteractHint(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleInteractHint(false);

            if (shopScreen.activeSelf)
            {
                ToggleShopScreen(false, false);
                Cursor.SetCursor(HUDController.instance.battleCursor, new Vector2(16, 48), CursorMode.Auto);
            }

            if (notEnoughText.activeSelf)
            {
                notEnoughText.SetActive(false);
            }
        }
    }

    void ToggleInteractHint(bool toggle)
    {
        canInteract = toggle;
        HUDController.instance.interactHint.enabled = toggle;
    }

    void ToggleShopScreen(bool screenToggle, bool hintToggle)
    {
        isOpened = screenToggle;
        shopScreen.SetActive(screenToggle);
        HUDController.instance.interactHint.enabled = hintToggle;
    }

    public IEnumerator ShowNotEnoughText()
    {
        notEnoughText.SetActive(true);
        yield return new WaitForSeconds(3);
        notEnoughText.SetActive(false);
    }
}
