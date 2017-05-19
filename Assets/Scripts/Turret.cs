using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret {

    private GameObject _TurrentGO;
    private Enemy _CurrentTarget;
    private List<Enemy> _EnemyTargets = new List<Enemy>();

    private InteractiveItem _Switch;

    private string _Type;

    private int _BullPS;

    public Turret(InteractiveItem s, string t)
    {
        _Switch = s;
        _Type = t;
        _EnemyTargets = new List<Enemy>();
        _Switch.Status = InteractiveItem.InteractiveStatus.On;
    }

    // Returns the current target of the turrent, or null if no current enemy to target
    public Enemy CurrentTarget() {
        Debug.Log("Current Target Activiated");
        if (_CurrentTarget != null && _CurrentTarget.Health <= 0) {
            _CurrentTarget = null;
            
            Debug.Log("Current Target is dead.");
        }

        if (_CurrentTarget == null) {
            if (EnemyTargets.Count > 0) {
                _CurrentTarget = _EnemyTargets[0];
                _EnemyTargets.RemoveAt(0);
                Debug.Log("New Target Assigned.");
            } else {
                _CurrentTarget = null;
            }
        }

        

        return _CurrentTarget;
    }

    public void addTarget(Enemy e) {
        _EnemyTargets.Add(e);
    }

    // This might be to much, the add should be enough.
    public List<Enemy> EnemyTargets {
        get { return _EnemyTargets; }
        set { _EnemyTargets = value; }
    }

    // Get the status from the switch, the switch controls its own status so we dont set from the turrent
    public InteractiveItem.InteractiveStatus Status {
        get { return _Switch.Status; }
    }

    public int BullPS {
        get {
            return _BullPS;
        }

        set {
            _BullPS = value;
        }
    }

    public GameObject TurrentGO {
        get {
            return _TurrentGO;
        }

        set {
            _TurrentGO = value;
        }
    }
}
