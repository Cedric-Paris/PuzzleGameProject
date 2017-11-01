using UnityEngine;

public class JumpProcedure : IMovementProcedure
{

    private static readonly float[] ROTATION_VALUES =
        {
            -85, -75, -70, -65, -55, -50, -45, -43, -40, -38, -35, -30, -25, -20, -15, -10, -5, -3,  0,
                3,   5,  10,  15,  20,  25,  30,  35,  38,  40,  43,  45,  47,  55,  60,  65, 70, 80, 83, 85
        };

    private Transform transform3DObject;
    private int currentPhase = 0;
    private const int START_PHASE_NUMBER = 11;
    private const int END_PHASE_NUMBER = 50;

    public void ProcessPhase(PlayerMovementController p)
    {
        if(transform3DObject == null)
        {
            transform3DObject = p.transform.Find("3DObject");
            if (transform3DObject == null)
                return;
        }
        if(currentPhase >= START_PHASE_NUMBER)
        {
            Vector3 anglesSave = transform3DObject.transform.eulerAngles;
            Vector3 angles = p.transform.rotation.eulerAngles;
            angles.x = ROTATION_VALUES[currentPhase-START_PHASE_NUMBER];
            p.transform.eulerAngles = angles;
            transform3DObject.eulerAngles = anglesSave;
        }
        currentPhase++;
        if (currentPhase >= END_PHASE_NUMBER)
        {
            p.RemoveMovementProcedure(this);
            OnMovementEnding(p);
        }
    }

    public void OnMovementFinishedForCurrentFrame(PlayerMovementController p)
    {
        if (p.transform.position.y < 0 && currentPhase > END_PHASE_NUMBER - 5)
        {
            p.RemoveMovementProcedure(this);
            OnMovementEnding(p);
        }
    }

    public void OnMovementEnding(PlayerMovementController p)
    {
        Vector3 anglesSave = transform3DObject.transform.eulerAngles;
        Vector3 angles = p.transform.eulerAngles;
        angles.x = 0;
        p.transform.eulerAngles = angles;
        transform3DObject.eulerAngles = anglesSave;
        p.transform.position = new Vector3(p.transform.position.x, 0, p.transform.position.z);
        transform3DObject = null;
        currentPhase = 0;
    }
}