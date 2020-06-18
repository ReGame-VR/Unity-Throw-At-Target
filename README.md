# Overview
 Unity context-switching and motor skill task involving Oculus Quest/Rift.
 Newer build with some major changes, made into its own repository to keep 
 old code separate and intact.
 
# Brief descriptions for each file

Accuracy Checker
- Class used by "target" objects
- Processes when a projectile hits or misses, and tells how much distance a projectile misses by

Data Handler
- Unfinished class
- Was working off a similar class in the CharlesHoodGrant task project

Global Control
- Class that sticks around in every scene
- Handles progression mode, calibration info, and other data needed between scenes

Ground Checker
- Class tied to projectile objects
- When colliding with another object, checks whether the object is tagged as a ground or as a target
- Also makes sure to not log a hit/miss multiple times if it bounces

Level Height Scale
- Class tied to Level Scaler objects (one per scene except for title and ending)
- Responsible for adjusting size of platform and distance of target, to correspond with calibration data (which it gets from GlobalControl)

Log Test Results
- Class tied to Log Manager object (one per scene except for title and ending)
- Logs results of a given trial to a textfile (ResultsLog.txt)
- Should be changed to a CSV
- Will likely be redone with switch to Quest

Menu Controller
- Class to handle Main Menu on title scene
- Processes which Progression Mode the player chose, and other related data, and sends to Global Control for storage

Progression Scoring
- Class tied to Progression Scoring object (one per scene except for title and ending)
- Processes whether the player has reached the given progression goal (from Global Control)
- Advances to next scene if so



# General User Guide
Title Scene
- Put player in headset
- Operator selects progression mode (if Performance, select necessary throws; if choice, select scene and time)


# Notes
- Choice progression unfinished
- Data saving unfinished
- I'm available to talk to if anything is really unclear. I tried to comment out a lot (partly for my own understanding), but can definitely clear things up. Also apologies that it's somewhat messy, working on my own for the first time on a project like this led to a few bad organizational habits. Let me know if you need anything, thank you!
