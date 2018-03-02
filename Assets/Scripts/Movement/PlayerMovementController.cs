using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utilities;

public class PlayerMovementController : MonoBehaviour
{
    private const int PASSAGE_STEP_100 = 5;
    private const float PASSAGE_STEP = 0.05f;
    public const int FIRST_PHASE_ID = 0;
    public const int MID_PHASE_ID = 10;
    public const int LAST_PHASE_ID = 19;
    private const float MAX_SPEED = 10f;
    private const float MIN_SPEED = 3f;
    private float speed = 3f;
    private float speedBoost = 0;
    private int currentPhaseId = MID_PHASE_ID;
    private float CurrentPhaseValue { get { return currentPhaseId * PASSAGE_STEP; } }
    private int NextPhaseId { get { return (currentPhaseId + 1 > LAST_PHASE_ID) ? FIRST_PHASE_ID : currentPhaseId + 1; } }

    private Vector3 currentDirectionVector  = Vector3.back;

    public Direction CurrentDirection
    {
        get { return ValueConverter.Vector3ToDirection(currentDirectionVector); }
        set { currentDirectionVector = ValueConverter.DirectionToVector3(value); }
    }

    public float BaseSpeed
    {
        get { return speed; }
        set
        {
            if (speed > MAX_SPEED) value = MAX_SPEED; else if (speed < MIN_SPEED) value = MIN_SPEED;
            speed = value;
        }
    }
    public float SpeedBost
    {
        get { return speedBoost; }
        set
        {
            if (value <= 0)
                speedBoost = 0;
            else
                speedBoost = value;
        }
    }
    public float Speed { get { return BaseSpeed + SpeedBost; } }

    public bool ShouldIgnoreNext { get; set; }

    private List<IMovementProcedure> movementProcedures;

    public delegate void OnPhasePointReached(PlayerMovementController sender, int phaseNumber);
    public delegate void OnNewSquareReached(PlayerMovementController sender, int x, int y);
    public event OnNewSquareReached NewSquareReached;
    public event OnPhasePointReached PhasePointReached;

    void Start()
    {
        this.movementProcedures = new List<IMovementProcedure>();
        SpeedBost = 0;
    }

	void Update ()
    {
        MoveWithPhases();
        MovementFinishedForCurrentFrame();
    }

    public void AddMovementProcedure(IMovementProcedure procedure)
    {
        this.movementProcedures.Add(procedure);
    }

    public void RemoveMovementProcedure(IMovementProcedure procedure)
    {
        this.movementProcedures.Remove(procedure);
    }

    public void ResetPhaseId()
    {
        currentPhaseId = CalculRelativePhaseIdWithGameObjectPosition();
    }

    private int CalculRelativePhaseIdWithGameObjectPosition()
    {
        Vector3 extractor = new Vector3((currentDirectionVector.x * currentDirectionVector.x), (currentDirectionVector.y * currentDirectionVector.y), (currentDirectionVector.z * currentDirectionVector.z));
        float value = (this.transform.position.x * extractor.x) + (this.transform.position.y * extractor.y) + (this.transform.position.z * extractor.z);
        int phaseNumber = Mathf.RoundToInt(value * 100);
        phaseNumber = phaseNumber - ((phaseNumber / 100) * 100);
        phaseNumber = phaseNumber / PASSAGE_STEP_100;
        if(CurrentDirection == Direction.South || CurrentDirection == Direction.West)
        {
            phaseNumber = LAST_PHASE_ID - phaseNumber;
        }
        return phaseNumber;

    }

    private float GetNextPassageValue(float currentPositionValue, bool verify = true)
    {
        double decimalPart;
        double integerPart = System.Math.Truncate(currentPositionValue);
        if (CurrentDirection == Direction.South || CurrentDirection == Direction.West)
        {
            if(currentPositionValue > 0)
            {
                decimalPart = (LAST_PHASE_ID - currentPhaseId) * PASSAGE_STEP;
            }
            else
            {
                decimalPart = NextPhaseId * PASSAGE_STEP * -1;
            }
        }
        else // Direction.North && Direction.East
        {
            
            if (currentPositionValue >= 0)
            {
                decimalPart = NextPhaseId * PASSAGE_STEP;
                if (NextPhaseId == FIRST_PHASE_ID)
                {
                    integerPart++;
                }
            }
            else
            {
                if(integerPart == 0 && NextPhaseId == FIRST_PHASE_ID)
                {
                    decimalPart = 0;
                }
                else
                {
                    decimalPart = (LAST_PHASE_ID - currentPhaseId) * -PASSAGE_STEP;
                }
            }
        }

        double resultValue = integerPart + decimalPart;
        if(verify && (System.Math.Abs(resultValue-currentPositionValue) > 2 * PASSAGE_STEP) )
        {
            if(CurrentDirection == Direction.South || CurrentDirection == Direction.West)
            {
                return GetNextPassageValue(currentPositionValue - (PASSAGE_STEP / 3), false);
            }
            else
            {
                return GetNextPassageValue(currentPositionValue - (PASSAGE_STEP / 3), false);
            }
        }  
        return (float)(resultValue);
    }

    private Vector3 NextPassagePoint()
    {
        /* Point d'intersection d'un plan et d'une droite
         *     | transform.position.x + forward.x * t
         * D = | transform.position.y + forward.y * t
         *     | transform.position.z + forward.z * t
         * 
         * P => x ou y ou z = prochain point 
         * 
         * Tirer la valeur de T
         * A partir de T on recupere x , y , z coordonnées du point d'intersection a partir
         * de l'équation parametrique de D
         */
        Vector3 extractor = new Vector3((currentDirectionVector.x * currentDirectionVector.x), (currentDirectionVector.y * currentDirectionVector.y), (currentDirectionVector.z * currentDirectionVector.z));
        float value = (this.transform.position.x * extractor.x) + (this.transform.position.y * extractor.y) + (this.transform.position.z * extractor.z);
        float nextPassageValue = GetNextPassageValue(value);

        float t, InterX, InterY, InterZ;
        if(currentDirectionVector.x != 0)
        {
            t = (nextPassageValue - this.transform.position.x) / this.transform.forward.x;
            InterX = nextPassageValue;
            InterZ = this.transform.position.z + (this.transform.forward.z * t);
        }
        else
        {
            t = (nextPassageValue - this.transform.position.z) / this.transform.forward.z;
            InterX = this.transform.position.x + (this.transform.forward.x * t);
            InterZ = nextPassageValue;
        }
        InterY = this.transform.position.y + (this.transform.forward.y * t);
        
        return new Vector3(InterX, InterY, InterZ);
    }

    private float DistanceToReachNextPassagePoint()
    {
        return Vector3.Distance(this.transform.position, NextPassagePoint());
    }

    private void Move()
    {
        this.transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
    }

    private void MoveWithPhases()
    {
        float moveLenght = Vector3.Distance(Vector3.zero, this.transform.forward * Speed * Time.deltaTime);
        Vector3 nextPoint = NextPassagePoint();
        float dToNextPoint = Vector3.Distance(this.transform.position, nextPoint);

        while (moveLenght > dToNextPoint)
        {
            this.transform.position = nextPoint;
            moveLenght -= dToNextPoint;
            //Phase event
            OnPhasePointReachedEvent();
            nextPoint = NextPassagePoint();
            dToNextPoint = Vector3.Distance(this.transform.position, nextPoint);
        }
        this.transform.Translate(Vector3.forward * moveLenght, Space.Self);
    }

    private Tuple<int, int> GetNewSquareReachedIndex()
    {
        int xIndex = 0, yIndex = 0;
        switch(CurrentDirection)
        {
            case Direction.North:
                xIndex = Mathf.FloorToInt(this.transform.position.x);
                yIndex = Mathf.RoundToInt(this.transform.position.z);
                break;
            case Direction.South:
                xIndex = Mathf.FloorToInt(this.transform.position.x);
                yIndex = Mathf.RoundToInt(this.transform.position.z) - 1;
                break;
            case Direction.East:
                xIndex = Mathf.RoundToInt(this.transform.position.x);
                yIndex = Mathf.FloorToInt(this.transform.position.z);
                break;
            case Direction.West:
                xIndex = Mathf.RoundToInt(this.transform.position.x) - 1;
                yIndex = Mathf.FloorToInt(this.transform.position.z);
                break;
        }
        return new Tuple<int, int>(xIndex, yIndex);
    }

    private void OnPhasePointReachedEvent()
    {
        currentPhaseId = NextPhaseId;
        if (currentPhaseId == FIRST_PHASE_ID && NewSquareReached != null)
        {
            if(ShouldIgnoreNext)
            {
                ShouldIgnoreNext = false;
            }
            else
            {
                var squareIndex = GetNewSquareReachedIndex();
                NewSquareReached(this, squareIndex.Item1, squareIndex.Item2);
            }
        }
        if (PhasePointReached != null)
        {
            PhasePointReached(this, currentPhaseId);
        }
        List<IMovementProcedure> l = new List<IMovementProcedure>(movementProcedures);
        foreach(IMovementProcedure m in l)
        {
            m.ProcessPhase(this);
        }
    }

    private void MovementFinishedForCurrentFrame()
    {
        List<IMovementProcedure> l = new List<IMovementProcedure>(movementProcedures);
        foreach (IMovementProcedure m in l)
        {
            m.OnMovementFinishedForCurrentFrame(this);
        }
    }

}
/* FLOATING POINT ISSUES
 * Extract from http://answers.unity3d.com/questions/121304/c-floating-point-confusion.html

 * "Most responders point out that floating-point is complicated, or at least more complicated than integer math. In particular
 * I was warned away from relying on == (and !=) between floats. Reasons given include: 1) That certain decimal numbers aren't
 * representable as floats (and therefore won't compare as equal. 0.1 being such a number) 2) Floating point arithmetic is therefore
 * order dependent (e.g. 1.0 - 0.1 + 0.1 can give different results depending on the order it is evaluated). 3) Floating point arithmetic
 * is rarely exact, even with high precision."
 */
