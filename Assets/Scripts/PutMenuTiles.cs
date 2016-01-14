using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PutMenuTiles : MonoBehaviour
{
    public int xMaximumEnPartantDroite;
    private List<Vector3> elementTilesAlreadySet = new List<Vector3>();
    private Menu menu;
    
    void Start ()
    {
        GameObject canvas = CanvasGetter.Canvas;
        if (canvas == null)
        {
            Debug.Log("Canvas null");
            return;
        }
        menu = canvas.GetComponentInChildren<Menu>();
        if (menu == null)
        {
            Debug.Log("Menu null");
            return;
        }
        // RECUPERER LES ELEMENTS SUR LA MAP DE BASE !!!
    }
	
	void Update () {
        if (Menu.SelectedObject == null || !SelectElementOnMenu.enableTileSet)
            return;
        Debug.Log("DETECTED SELECTED OBJECT");
        Camera c = Camera.main;
        Vector3 worldPos;
        foreach (Touch t in Input.touches)
        {
            if (t.position.x > Screen.width - xMaximumEnPartantDroite || menu.Count[Menu.SelectedObjectsDraggableElementType] <= 0)
                continue;
            worldPos = c.ScreenToWorldPoint(t.position);
            worldPos.x = CalculDemiLePlusProche(worldPos.x);
            worldPos.y = CalculDemiLePlusProche(worldPos.y);
            worldPos.z = 0;
            if (elementTilesAlreadySet.Contains(worldPos))
                continue;
            elementTilesAlreadySet.Add(worldPos);
            ((GameObject)Instantiate(Menu.SelectedObject, worldPos, Quaternion.identity)).transform.SetParent(this.gameObject.transform);
            menu.DownTheRightCount(Menu.SelectedObjectsDraggableElementType);
        }

        // POUR LES TESTS SUR PC
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICK RECU !!");
            if (Input.mousePosition.x > Screen.width - xMaximumEnPartantDroite || menu.Count[Menu.SelectedObjectsDraggableElementType] <=0)
                return;
            worldPos = c.ScreenToWorldPoint(Input.mousePosition);
            worldPos.x = CalculDemiLePlusProche(worldPos.x);
            worldPos.y = CalculDemiLePlusProche(worldPos.y);
            worldPos.z = 0;
            if (elementTilesAlreadySet.Contains(worldPos))
                return;
            elementTilesAlreadySet.Add(worldPos);
            ((GameObject)Instantiate(Menu.SelectedObject, worldPos, Quaternion.identity)).transform.SetParent(this.gameObject.transform);
            menu.DownTheRightCount(Menu.SelectedObjectsDraggableElementType);
        }
    }

    private float CalculDemiLePlusProche(float value)
    {
        if (value < 0)
            return ((int)value) - 0.5f;
        return ((int)value) + 0.5f;
    }
}
