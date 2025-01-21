using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class lightInteractionPackage
{
    public Light light;
    public lightInteractionModes interactionMode;
}

[Serializable]
public class movingPlatformInteractionPackage
{
    public GameObject movingPlatform;
    public moverInteractionModes moverInteractionMode;
}

[Serializable]
public class teleporterInteractionPackage
{
    public TeleporterVolume teleporter;
    public teleporterInteractionModes interactionMode;
}

[Serializable]
public class conveyorInteractionPackage
{
    public ConveyorBehavior conveyor;    
    public conveyorInteractionModes interactionMode;
}

public class ComboControllerButtonBehavior : InteractiveObject
{
    [Header("Lights")]
    public List<lightInteractionPackage> lights;    

    [Header("Moving Platforms")]
    public List<movingPlatformInteractionPackage> movingPlatforms;
    
    [Header("Conveyor Belts")]
    public List<conveyorInteractionPackage> conveyors;

    [Header("Teleporters")]
    public List<teleporterInteractionPackage> teleporters;

    public override void interact()
    {
        base.interact();
        if (UseOnce && _used)
            return;
        if (!isEnabled)
            return;
        foreach (lightInteractionPackage lip in lights)
        {
            if (lip != null)
            {
                if (lip.light!=null)
                {
                    
                        switch (lip.interactionMode)
                        {
                            case lightInteractionModes.toggleOnOff:
                            lip.light.enabled = !lip.light.enabled;
                                break;
                            case lightInteractionModes.turnOff:
                            lip.light.enabled = false;
                                break;
                            case lightInteractionModes.turnOn:
                            lip.light.enabled = true;
                                break;
                        }
                    
                }
            }
        }
        foreach (movingPlatformInteractionPackage mpip in movingPlatforms)
        {
            if (mpip != null)
            {
                GameObject mp = mpip.movingPlatform;
                if (mp != null)
                {
                    MoverBehavior mb = mp.GetComponent<MoverBehavior>();
                    if (mb != null)
                    {
                        mb.processInteractionInput(mpip.moverInteractionMode);
                    }

                    AdvancedMoverBehavior amb = mp.GetComponent<AdvancedMoverBehavior>();
                    if (amb != null)
                    {
                        amb.processInteractionInput(mpip.moverInteractionMode);
                    }

                    RotatingMoverBehavior rmb = mp.GetComponent<RotatingMoverBehavior>();
                    if (rmb != null)
                    {
                        rmb.processInteractionInput(mpip.moverInteractionMode);
                    }

                }
            }
        }
        foreach (conveyorInteractionPackage cip in conveyors)
        {
            if(cip!=null)
            {
                if(cip.conveyor!=null)
                {
                    cip.conveyor.processsInteraction(cip.interactionMode);
                }
            }
        }
        foreach (teleporterInteractionPackage tpip in teleporters)
        {
            if(tpip!=null)
            {
                if(tpip.teleporter!=null)
                {
                    tpip.teleporter.ProcessInteraction(tpip.interactionMode);
                }
            }
        }
    }
}