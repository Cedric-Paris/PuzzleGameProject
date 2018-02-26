using UnityEngine;
using Assets.Scripts.Utilities;

public class TurnProcedure : IMovementProcedure {

    private static readonly int[] ROTATION_VALUES = {
                                                    //From NORTH
                                                        0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   //To North
                                                        0,   0,   0,   0,   0,   0,   0,   0,   0,   0,   180, //To South
                                                        0,   0,   0,   0,   0,   0,   20,  35,  55,  70,  90,  //To East
                                                        0,   0,   0,   0,   0,   0,   340, 325, 305, 290, 270, //To West
                                                    // From SOUTH
                                                        180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 0,   //To North
                                                        180, 180, 180, 180, 180, 180, 180, 180, 180, 180, 180, //To South
                                                        180, 180, 180, 180, 180, 180, 160, 145, 125, 110, 90,  //To East
                                                        180, 180, 180, 180, 180, 180, 200, 215, 235, 250, 270, //To West
                                                    // From EAST
                                                        90,  90,  90,  90,  90,  90,  70,  55,  35,  20,  0,   //To North
                                                        90,  90,  90,  90,  90,  90,  110, 125, 145, 160, 180, //To South
                                                        90,  90,  90,  90,  90,  90,  90,  90,  90,  90,  90,  //To East
                                                        90,  90,  90,  90,  90,  90,  90,  90,  90,  90,  270, //To West
                                                    // From WEST
                                                        270, 270, 270, 270, 270, 270, 290, 305, 325, 340, 0,   //To North
                                                        270, 270, 270, 270, 270, 270, 250, 235, 215, 200, 180, //To South
                                                        270, 270, 250, 270, 270, 270, 270, 270, 270, 270, 90,  //To East
                                                        270, 270, 270, 270, 270, 270, 270, 270, 270, 270, 270  //To West
                                                    };

    public Direction TargetDirection { get; private set; }

    private int currentPhase = 0;
    private const int END_PHASE_NUMBER = 11;

    public TurnProcedure(Direction targetDirection)
    {
        TargetDirection = targetDirection;
    }

    private int GetRotationValue(Direction currentDirection, Direction endDirection, int phaseId)
    {
        int valuePosition = (((int)currentDirection) * (END_PHASE_NUMBER * 4)) + (((int)endDirection) * END_PHASE_NUMBER) + phaseId;
        return ROTATION_VALUES[valuePosition];
    }

    public void ProcessPhase(PlayerMovementController p)
    {
        Vector3 angles = p.transform.rotation.eulerAngles;
        angles.y = GetRotationValue(p.CurrentDirection, TargetDirection, currentPhase);
        p.transform.eulerAngles = angles;
        currentPhase++;
        if(currentPhase >= END_PHASE_NUMBER)
        {
            p.RemoveMovementProcedure(this);
            OnMovementEnding(p);
        }
    }

    public void OnMovementFinishedForCurrentFrame(PlayerMovementController p) { }

    //Utiliser pour appliquer des effets sur le Player apres les mouvements (changement direction etc.)
    public void OnMovementEnding(PlayerMovementController p)
    {
        float xSave = p.transform.rotation.eulerAngles.x;
        p.transform.LookAt(p.transform.position + ValueConverter.DirectionToVector3(TargetDirection));
        Vector3 angles = p.transform.rotation.eulerAngles;
        angles.x = xSave;
        p.transform.eulerAngles = angles;
        p.CurrentDirection = TargetDirection;
        p.ResetPhaseId(); // We need to reset the phase Id because it can change in a new direction
        currentPhase = 0;
    }

}
