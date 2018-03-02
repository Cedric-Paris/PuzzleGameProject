using UnityEngine;

public class SinkProcedure : IMovementProcedure
{
    private static readonly float[] ROTATION_VALUES = { 5, 12, 17, 26, 35, 40, 50, 55, 60, 62, 65, 70 };
    private const int END_PHASE_NUMBER = 12;

    private Transform transform3DObject;
    private Player playerAttachedTo;
    private int currentPhase = 0;

    public SinkProcedure(Player attachedTo)
    {
        playerAttachedTo = attachedTo;
    }

    public void ProcessPhase(PlayerMovementController p)
    {
        if (transform3DObject == null)
        {
            transform3DObject = p.transform.Find("3DObject");
            if (transform3DObject == null)
                return;
        }
        Vector3 anglesSave = transform3DObject.transform.eulerAngles;
        Vector3 angles = p.transform.rotation.eulerAngles;
        angles.x = ROTATION_VALUES[currentPhase];
        p.transform.eulerAngles = angles;
        transform3DObject.eulerAngles = anglesSave;

        currentPhase++;
        if (currentPhase >= END_PHASE_NUMBER)
        {
            p.RemoveMovementProcedure(this);
            OnMovementEnding(p);
        }
    }

    public void OnMovementFinishedForCurrentFrame(PlayerMovementController p) { }

    public void OnMovementEnding(PlayerMovementController p)
    {
        playerAttachedTo.Kill();
    }
}
