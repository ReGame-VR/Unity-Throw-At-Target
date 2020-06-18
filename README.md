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
- Also handles swapping between scenes with a fade

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
- Tells Level Height Scale to advance to next scene if so

Projectile Manager
- Class tied to Projectile Manager object (one per scene except for title and ending)
- Keeps track of initial positions of projectiles, for resetting purposes

ReadWriteCSV
- Unused class
- Also borrowed from the CharlesHoodGrant task repo, originally from a different tutorial linked in the file
- Meant to be used to simply read and write to CSV, but never managed to implement properly

Recalibrate Height
- Class tied to Height Calibration object in Calibration scene
- Handles the height and arm length measurements of the player, before sending those to Global Control upon finishing calibrating

Simple Timer
- Unfinished class
- Meant to simply run a timer (for choice progression) and execute a function on finish (change scene)

# Progression Modes
Performance
- Operator selects X number of successes out of last Y throws (ex. 2 out of 5)
- Player must succeed in that number of throws to move on (ex. 2/5 -> success, miss, miss, miss, success -> continue)
- Throw information is selected on title screen by operator, and stored in Global Control since it is constant

Random
- Player has to complete a random number of total throws, regardless of outcome, re-rolled each scene
- Throw information is decided upon entering a scene with Progression Scorer object (Classroom, Park, Moon), and stored in that object since it changes with each scene

Choice [UNFINISHED]
- Operator selects a scene and amount of time for the player
- Player goes to that scene, plays for that amount of time, and then is transported back to title screen
- This can loop as many times as operator needs
- Scene/Time information is selected on title screen by operator, and stored in Global Control since it is constant

# General User Guide
Title Scene
- Put player in headset
- Operator selects progression mode (if Performance, select necessary throws; if choice, select scene and time)
- Clicks "Continue" button

Calibration
- Operator asks player to stand at a position comfortable to pick up an object
- Operator presses RightShift to calibrate (or player, for debugging, can press Y) to lock in calibration data
- Player can practice in this scene until ready to move on, to reset the projectile operator can press Return (or player, for debugging, can press A)
- To progress to the next scene (also applies to every scene except for title and ending), operator presses KeypadEnter (or player, for debugging, can press X)

Classroom/Park/Moon
- Player must fulfill progression requirement to move on
- Throw object as normal, operator can assist with player as needed
- The same keybinds apply for resetting a projectile after throw and advancing to next scene

End
- Player reaches end, can take off headset

# Notes
- Choice progression unfinished
- Data saving unfinished
- I'm available to talk to if anything is really unclear. I tried to comment out a lot (partly for my own understanding), but can definitely clear things up. Also apologies that it's somewhat messy, working on my own for the first time on a project like this led to a few bad organizational habits. Let me know if you need anything, thank you!
