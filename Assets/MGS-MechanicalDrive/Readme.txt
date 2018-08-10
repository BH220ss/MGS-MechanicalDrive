==========================================================================
  Copyright © 2017-2018 Mogoson. All rights reserved.
  Name: MGS-Machinery
  Author: Mogoson   Version: 0.1.0   Date: 8/10/2018
==========================================================================
  [Summary]
    Unity plugin for binding mechanical drive in scene.
--------------------------------------------------------------------------
  [Demand]
    Binding mesh Gear.
    Binding worm gear.
    Binding belt flywheel.
    Binding vibrator.
    Binding differential.
    Binding transmission.
--------------------------------------------------------------------------
  [Environment]
    Unity 5.0 or above.
    .Net Framework 3.0 or above.
--------------------------------------------------------------------------
  [Achieve]
    Gear : Gear rotate around axis Z.

    Axle : Axle rotate around axis Z.

    CoaxialGear : Coaxial gear with the same axis as another gear.

    WormGear : Worm gear mechanism.

    WormShaft : Worm shaft mechanism.

    Belt : Move texture UV on X axis.

    LinearVibrator : Reciprocating motion on Z axis.

    CentrifugalVibrator : Eccentric motion around Z axis.

    Synchronizer : All mechanisms of the synchronizer driven by same
    velocity.

    Transmission : All mechanisms of the Transmission driven by
    proportional velocity.

    Engine : Unified engine drive all mechanisms. 

    Differential : Ordinary differential.
--------------------------------------------------------------------------
  [Suggest]
    The radius of gear should be set precisely.

    UV of belt model should be transverse arrangement, the texture of
    belt is preferably all sides continuous.

    Make sure the gear engages perfectly with the worm when building
    model.

    The amplitude radius of CentrifugalVibrator or LinearVibrator
    usually set a small value.
--------------------------------------------------------------------------
  [Demo]
    Prefabs in the path "MGS-Machinery/Prefabs" provide reference
    to you.

    Demos in the path "MGS-Machinery/Scenes" provide reference to
    you.
--------------------------------------------------------------------------
  [Resource]
    https://github.com/mogoson/MGS-MechanicalDrive.
--------------------------------------------------------------------------
  [Contact]
    If you have any questions, feel free to contact me at mogoson@outlook.com.
--------------------------------------------------------------------------