using UnityEngine;

/// <summary>
/// The purpose of this script is to handle opening and closing the UI
/// </summary>
public class UIManager : MonoBehaviour
{
    private void Start()
    {
        //TODO
        // call the ClosePanel() function
        ClosePanel();
    }
    
    public void ClosePanel()
    {
        //TODO
        // Set this gameObject's visibility to false
        gameObject.SetActive(false);
    }

    public void OpenPanel()
    {
        //TODO
        // Set this gameObject's visibility to true
        gameObject.SetActive(true);
    }
}
