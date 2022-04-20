using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class GameStepManager
    {
        private readonly Random _random;

        public GameStep CurrentStep;

        /// <summary>
        /// Handle game steps and manage turn structure 
        /// </summary>
        /// <param name="random"></param>
        public GameStepManager(Random random)
        {
            _random = random;
            CurrentStep = GameStep.First;
        }

        public CustomColor GenerateTargetColor()
        {
            var values = Enum.GetValues(typeof(CustomColor));
            return (CustomColor)values.GetValue(_random.Next(values.Length - 1));
        }
    }
}
