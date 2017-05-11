using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private double operational_power_usage; // This is how much power they are using to turn on devices
    private double reserved_power_usage; // How much power they are using if everything is turned of
    private int materials; // How many materials has the player collected.
    private double core_damage; // The nanomachinces deal damage to the core, when it takes enough damage, the core overloads and the ship explodes.
    private bool game_over;

    // Use this for initialization
    void Start () {
        operational_power_usage = 0;
        reserved_power_usage = 50;
        materials = 0;
        core_damage = 0;
        game_over = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(core_damage > 100) {
            game_over = true;
        }
	}

    public bool GameOver {
        get { return game_over; }
    }

    // deal damage to core, enemies should call this
    public void damageCore(double damage) {
        if (damage > 0) {
            core_damage += damage;
        }
    }

    // How many materials on hand.
    public int Materials {
        get {return materials;}
    }


    // When an enemie dies, this function should be called with how much the enemy was worth
    public void increaseMaterials(int mats) {
        // check to make sure its not negative.
        if (mats > 0) {
            materials += mats;
        }
    }

    // It costs materials to repair ship systems, all values will be shown as positive
    public void spendMaterials(int mats) {
        // check to make sure its not negative.
        if (mats > 0) {
            materials -= mats;
        }
    }

    // How much power they are using is operational+reserved
    public double PowerUsage {
        get { return operational_power_usage + reserved_power_usage; }
    }

    // UI will need to know the two seperate amounts.
    public double OperationalPowerUsage {
        get { return operational_power_usage; }
    }

    public double ReservedPowerUsage {
        get { return reserved_power_usage; }
    }


    // When a system is repaired, its turned on by default, so this should be called on interactiveitem repair is 100%
    public void increasePowerUsage(double operational, double reserved) {
        if(operational >= 0 && reserved >= 0) {
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
