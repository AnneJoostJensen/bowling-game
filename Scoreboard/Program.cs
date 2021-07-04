using System;
using System.Collections.Generic;
using System.Linq;

namespace Bowling
{
    public class Game
    {
        static void Main(string[] args)
        {
            Scoreboard scoreboard = new Scoreboard(new ScoreboardFrames());
            Console.WriteLine("****** Start bowling ******");
            while (scoreboard.getScoreboardFrames().canAddFrame()) {
                Frame frame = scoreboard.getScoreboardFrames().addFrame();
                while (frame.getFrameRolls().canRoll())
                {
                    Console.ReadKey();

                    // TODO let the user decide how many pins to knock down in each roll
                    Roll roll = frame.getFrameRolls().doRoll(1);

                    Console.WriteLine("Frame " + scoreboard.getScoreboardFrames().getFrameCount());
                    Console.WriteLine("Roll " + frame.getFrameRolls().getRollCount());
                    Console.WriteLine("Knocked down pins " + roll.getKnockedDownPins());
                    Console.WriteLine("Total score " + scoreboard.getTotalScore());
                }
            }
            Console.WriteLine("****** The End ******");
            Console.ReadKey();
        }
    }

    public class Scoreboard
    {
        ScoreboardFrames _frames;

        public Scoreboard(ScoreboardFrames frames)
        {
            _frames = frames;
        }
        public ScoreboardFrames getScoreboardFrames()
        {
            return _frames;
        }

        public int getTotalScore()
        {
            return _frames.getTotalScore();
        }
    }

    // TODO add unit tests for the ScoreboardFrames class
    public class ScoreboardFrames
    {
        static int NUMBER_OF_FRAMES = 10;
        List<Frame> _frames = new List<Frame>(NUMBER_OF_FRAMES);

        public bool canAddFrame()
        {
            return _frames.Count < NUMBER_OF_FRAMES;
        }

        public Frame addFrame()
        {
            Frame frame = isLastFrame() ?
                // TODO new Frame(new LastFrameRolls(), new FrameScore())
                new Frame(new FrameRolls(true), new FrameScore()) :
                // TODO new Frame(new FrameRolls(), new FrameScore()) 
                new Frame(new FrameRolls(false), new FrameScore());
            _frames.Add(frame);
            return frame;
        }

        public int getFrameCount()
        {
            return _frames.Count;
        }

        private bool isLastFrame()
        {
            return _frames.Count == NUMBER_OF_FRAMES - 1;
        }

        public Frame getFrame(int index)
        {
            return _frames.ElementAt(index);
        }

        public int getTotalScore()
        {
            int totalScore = 0;
            for (int i = 0; i < getFrameCount(); i++)
            {
                if (i < getFrameCount() - 1)
                {
                    Frame nextFrame = getFrame(i + 1);
                    totalScore += getFrame(i).getScore();
                    totalScore += getFrame(i).getBonus(nextFrame);
                }
                else
                {
                    totalScore += getFrame(i).getScore();
                }
            }
            return totalScore;
        }
    }

    // TODO add unit tests for the Frame class
    public class Frame
    {
        protected FrameRolls _rolls;
        protected FrameScore _score;

        public Frame(FrameRolls rolls, FrameScore score)
        {
            _rolls = rolls;
            _score = score;
        }

        public int getScore()
        {
            return _score.getScore(_rolls);
        }

        public int getBonus(Frame nextFrame)
        {
            return _score.getBonus(nextFrame, _rolls);
        }

        public int getFirstRollScore()
        {
            return _score.getFirstRollScore(_rolls);
        }

        public FrameRolls getFrameRolls()
        {
            return _rolls;
        }
    }

    // TODO take everything with extra roll out of the FrameRolls class.
    // Create new class LastFrameRolls which extends the FrameRolls class.
    // Have everything with extra roll inside the LastFrameRolls class.
    // There is no need for the "regular" FrameRolls to know anything about extra roll.
    public class FrameRolls
    {
        protected static int NUMBER_OF_ROLLS = 2;
        protected static int NUMBER_OF_PINS = 10;
        protected List<Roll> _rolls;
        bool _extraRoll;

        public FrameRolls(bool extraRoll)
        {
            _rolls = new List<Roll>(NUMBER_OF_ROLLS);
            _extraRoll = extraRoll;
        }

        public bool canRoll()
        {
            bool moreRolls = getRollCount() < NUMBER_OF_ROLLS;
            return _extraRoll ? moreRolls || canExtraRoll() : moreRolls && !hasStrike();
        }

        public Roll doRoll(int knockedDownPins)
        {
            Roll roll = new Roll(knockedDownPins, NUMBER_OF_PINS);
            _rolls.Add(roll);
            return roll;
        }

        public int getRollCount()
        {
            return _rolls.Count;
        }

        public Roll getRoll(int index)
        {
            if (index < 0 || index > getRollCount() - 1)
                throw new Exception("Roll index out of bound");
            return _rolls.ElementAt(index);
        }

        public bool hasStrike()
        {
            return getRollCount() == 1 && getRoll(0).getKnockedDownPins() == NUMBER_OF_PINS;
        }

        public bool hasSpare()
        {
            return getRollCount() == 2 &&
                 _rolls.First().getKnockedDownPins() != NUMBER_OF_PINS &&
                 _rolls.First().getKnockedDownPins() + _rolls.Last().getKnockedDownPins() == NUMBER_OF_PINS;
        }

        public Roll getLastRoll()
        {
            if (getRollCount() == NUMBER_OF_ROLLS)
            {
                return _rolls.Last();
            }
            return null;
        }

        public bool canExtraRoll()
        {
            if (!_extraRoll || getRollCount() == NUMBER_OF_ROLLS + 1)
            {
                return false;
            }
            Roll lastRoll = getLastRoll();
            bool isLastRollStrike = lastRoll != null && lastRoll.knockedDownAllPins();
            return hasSpare() || isLastRollStrike;
        }
        // TODO move getScore to the FrameScore.
        // The roll does not need to know about the score. It should
        // be the responsibility of the FrameScore.
        public int getScore() {
            int score = 0;
            _rolls.ForEach(roll => score += roll.getScore());
            return score;
        }
    }

    // TODO add unit tests for the FrameScore class
    public class FrameScore
    {
        public int getBonus(Frame nextFrame, FrameRolls frameRolls)
        {
            if (frameRolls.hasStrike())
            {
                // TODO if the next frame has stike we also need the score 
                // from the first roll in the next-next frame 
                return nextFrame.getScore(); 
            }
            if (frameRolls.hasSpare())
            {
                return nextFrame.getFirstRollScore();
            }
            return 0;
        }

        public int getScore(FrameRolls frameRolls)
        {
            return frameRolls.getScore();
        }

        public int getFirstRollScore(FrameRolls frameRolls)
        {
            return frameRolls.getRollCount() > 0 ? frameRolls.getRoll(0).getScore() : 0;
        }
    }

    public class Roll
    {
        int _knockedDownPins;
        int _pins;

        public Roll(int knockedDownPins, int pins)
        {
            if (knockedDownPins < 0 || knockedDownPins > pins)
                throw new Exception("Number of knocked down pins must be between 0 and " + pins);
            _knockedDownPins = knockedDownPins;
            _pins = pins;
        }

        public int getKnockedDownPins()
        {
            return _knockedDownPins;
        }
        // TODO move getScore to the FrameScore.
        // The roll does not need to know about the score. It should
        // be the responsibility of the FrameScore.
        public int getScore()
        {
            return getKnockedDownPins();
        }

        public bool knockedDownAllPins()
        {
            return getKnockedDownPins() == _pins;
        }
    }
}
