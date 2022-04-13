using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class GameStepManager
    {
        private readonly Random _random;

        /// <summary>
        /// Handle game steps and manage turn structure 
        /// </summary>
        /// <param name="random"></param>
        public GameStepManager(Random random)
        {
            _random = random;
        }

        public CustomColor GenerateTargetColor()
        {
            var values = Enum.GetValues(typeof(CustomColor));
            return (CustomColor)values.GetValue(_random.Next(values.Length));
        }
    }
}
