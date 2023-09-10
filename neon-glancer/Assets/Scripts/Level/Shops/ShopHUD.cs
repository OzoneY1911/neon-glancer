using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopHUD : MonoBehaviour
{
    [SerializeField] GameObject shopScreen;

    [SerializeField] GameObject notEnoughText;

    bool canInteract;

    Coroutine notEnoughCoroutine;

    void Awake()
    {
        shopScreen.SetActive(false);
    }

    void LateUpdate()
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

    void ToggleShopScreen(bool screenToggle, bool hintToggle)
    {
        PlayerMovement.instance.canRotate = !screenToggle;
        shopScreen.SetActive(screenToggle);
        HUDController.instance.interactHint.enabled = hintToggle;
    }

    void ToggleInteractHint(bool toggle)
    {
        canInteract = toggle;
        HUDController.instance.interactHint.enabled = toggle;
    }

    public bool CheckPrice(int price)
    {
        if (PlayerStats.instance.neon >= price)
        {
            PlayerStats.instance.ChangeNeon(-price);
            HUDController.instance.UpdateNeonText();

            return true;
        }
        else
        {
            if (notEnoughCoroutine != null)
            {
                StopCoroutine(notEnoughCoroutine);
            }
            notEnoughCoroutine = StartCoroutine(ShowNotEnoughText());

            return false;
        }
    }

    public IEnumerator ShowNotEnoughText()
    {
        notEnoughText.SetActive(true);
        yield return new WaitForSeconds(3);
        notEnoughText.SetActive(false);
    }
}
