using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;

    public void Start()
    {
        this.gameObject.transform.SetParent(MenuManager.instance.transform);
    }
    /// <summary>
    /// Deactivate the panel of the menu
    /// </summary>
    public void Close()
    {
        menu.SetActive(false);
    }
    /// <summary>
    /// Activate the panel of the menu
    /// </summary>
    public virtual void Open()
    {
        menu.SetActive(true);
    }
    /// <summary>
    /// Back to previus opened menu, remove the menu from the stack in the menuManager
    /// </summary>
    public virtual void Exit()
    {
        MenuManager.instance.Back();
    }
}
