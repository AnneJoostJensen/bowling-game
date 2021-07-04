The program consists of a bowling game.
Start the program and press any key to roll the bowling ball.

The game (Game) has a scoreboard (Scoreboard) which holds the scoreboard frames (ScoreboardFrames).
The game continues if it can add frames.
The scoreboard frames have the responsibility of deciding whether frames can be added or not.
The scoreboard frames consist of 10 frames (list of Frame).
Each frame holds the frame rolls (FrameRolls) and the frame score (FrameScore).
When the game has added a frame, the game keeps rolling the bowling ball as many times as it can inside that frame.
The frame rolls have the responsibility of deciding whether the bowling ball can be rolled or not.
The frame rolls contain the rolls (list of Roll).
The last frame roll is setup to have an extra roll in case the user rolls a spare or strikes.
After each roll (Roll) the frame number, roll number, how many pins were knocked down and the total score is printed.
The frame score calculates the score and the bonus of the frame. The score depends on the pins knocked down in each roll.
The bonus is given if the frame rolls have a strike or spare and depends on the pins knocked down in the next frame(s).
Each roll holds the knocked down pins and can calculate if all the pins were knocked down in that roll.

TODOs are added to the program. These are added because of lack of time. The TODOs describe what needs some extra attention and
describe some design changes that could improve the code.
