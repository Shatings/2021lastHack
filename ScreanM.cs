using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreanM : MonoBehaviour
{
    public bool window = true;
    public List<int> winsizex = new List<int>();
    public List<int> winsizey = new List<int>();
    public Dropdown dropdown;
    public Dropdown windowdrop;
    public GameObject canvas;
    
    public void SceenChange()
    {
        WindowCheck();
        Debug.Log("x" + winsizex[dropdown.value] + "y" + winsizey[dropdown.value] + "window" + window);
        Screen.SetResolution(winsizex[dropdown.value], winsizey[dropdown.value], window);
    }
    public void WindowCheck()
    {
        if (windowdrop.value == 0)
        {
            window = true;
        }
        else
        {
            window = false;
        }
    }
  
    public void Canvas()
    {
        Debug.Log(""+canvas.activeSelf);
        canvas.SetActive(canvas.activeSelf!=true);
    }
   
}
