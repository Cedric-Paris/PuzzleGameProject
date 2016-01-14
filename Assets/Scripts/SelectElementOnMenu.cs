using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectElementOnMenu : MonoBehaviour {

    public static bool enableTileSet = true; // Si false on ne placera pas les éléments sur la map => Pour gérer le zoom principalement.

    public void OnSelectElement(Button selectedButton)
    {
        GameObjectHandler objectToSummon = this.GetComponent<GameObjectHandler>();
        DraggableElementHandler objectToSummonsType = this.GetComponent<DraggableElementHandler>();
        if (objectToSummon != null && objectToSummon.GameObject != null && objectToSummonsType != null)
        {
            Menu.SelectedObject = objectToSummon.GameObject;
            Menu.SelectedObjectsDraggableElementType = objectToSummonsType.DraggableElementType;
        }
    }
}
