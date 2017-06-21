using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    
    public float TimeForEnginesToIgnite; // How long does it take for the engines to be ready for takeoff.
    public int Materials; // How many materials has the player collected.
    public double Core; // The nanomachinces deal damage to the core, when it takes enough damage, the core overloads and the ship explodes.

    private double operational_power_usage; // This is how much power they are using to turn on devices
    private double reserved_power_usage; // How much power they are using if everything is turned of

    private bool game_over;
    private bool game_won;


    // Use this for initialization
    void Start () {
        operational_power_usage = 0;
        reserved_power_usage = 0;
        game_over = false;
        game_won = false;
        _StartCountDown = false;
        _TimeToLaunch = TimeForEnginesToIgnite;

    }
	
	// Update is called once per frame
	void Update () {
		if(Core == 0) {
            game_over = true;
        }
        if (StartCountDown == true) {
            Countdown();
        }

    }

    private float _TimeToLaunch;
    private bool _StartCountDown;

    public void Countdown() {

        if (!game_over) {
            _TimeToLaunch -= Time.deltaTime;

            if (_TimeToLaunch <= 0) {
                _TimeToLaunch = 0;
                game_won = true;
                game_over = true;
            }
        }

    }

    public bool GameWon {
        get { return game_won; }
    }
    public bool GameOver {
        get { return game_over; }
    }

    // deal damage to core, enemies should call this
    public void damageCore(double damage) {
        if (damage > 0) {
            Core -= damage;
        }
        if(Core < 0) {
            Core = 0;
        }
    }

    // When an enemie dies, this function should be called with how much the enemy was worth
    public void increaseMaterials(int mats) {
        // check to make sure its not negative.
        if (mats > 0) {
            Materials += mats;
        }
    }

    // It costs materials to repair ship systems, all values will be shown as positive
    public void spendMaterials(int mats) {
        // check to make sure its not negative.
        if (mats > 0) {
            Materials -= mats;
        }
    }

    public void ResetPowerUsage() {
        operational_power_usage = 0;
        reserved_power_usage = 0;
    }


    // UI will need to know the two seperate amounts.
    public double OperationalPowerUsage {
        get { return operational_power_usage; }
    }

    public double ReservedPowerUsage {
        get { return reserved_power_usage; }
    }

    public int TimeToLaunch {
        get {
            return Mathf.FloorToInt(_TimeToLaunch);
        }
    }

    public bool StartCountDown {
        get {
            return _StartCountDown;
        }

        set {
            _StartCountDown = value;
        }
    }


    // When a system is repaired, its turned on by default, so this should be called on interactiveitem repair is 100%
    public void increasePowerUsage(double operational, double reserved) {
        if(operational >= 0 || reserved >= 0) {
            operational_power_usage += operational;
            reserved_power_usage += reserved;
        }
    }

    // We can only reduce power from operational, reserved is locked in
    public void decreasePowerUsage(double operational) {
        
        if(operational > 0) {
            // it can't go below 0, for now lets set it to 0 if it would go below 0.
            if ((operational_power_usage - operational) >= 0) {
                operational_power_usage -= operational;
            } else {
                operational_power_usage = 0;
            }
        }
    }

    
}
