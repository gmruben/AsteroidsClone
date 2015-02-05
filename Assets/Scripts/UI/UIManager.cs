using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    private Camera cachedCamera;
    
    public static UIManager instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.Find("UICamera").GetComponent<UIManager>();
            return _instance;
        }
    }

    void Start()
    {
        cachedCamera = GetComponent<Camera>();
    }
	
	public bool isMouseOnGUI()
	{
		return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
	}
}