using UnityEngine;

public class ActionInMenu : MonoBehaviour
{
    private const int ANIM_SPEED = 20;
    private const int ANIM_MIN_SCALE = 60;
    private const int ANIM_MAX_SCALE = 70;

    private bool isAnimated = false;
    private int factor = 1;

    public bool IsAnimated
    {
        get { return isAnimated; }
        set
        {
            if (value == false)
            {
                this.transform.localScale = new Vector3(ANIM_MIN_SCALE, ANIM_MIN_SCALE, ANIM_MIN_SCALE);
                factor = 1;
            }
            isAnimated = value;
        }
    }

	void Update ()
    {
        this.transform.eulerAngles = Vector3.zero;
        if (IsAnimated)
        {
            float newScale = this.transform.localScale.x + (Time.deltaTime * ANIM_SPEED * factor);
            if (newScale > ANIM_MAX_SCALE)
            {
                newScale = ANIM_MAX_SCALE - (newScale - ANIM_MAX_SCALE);
                factor *= -1;

            }
            else if (newScale < ANIM_MIN_SCALE)
            {
                newScale = ANIM_MIN_SCALE + (ANIM_MIN_SCALE - newScale);
                factor *= -1;
            }
            this.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
