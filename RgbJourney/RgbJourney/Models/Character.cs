﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.Models
{
    public class Character
    {
        public int MaxStamina { get; set; }
        public int CurrentStamina { get; set; }
        public int RedKeys { get; set; }
        public int GreenKeys { get; set; }
        public int BlueKeys { get; set; }

        public Character(int maxStamina)
        {
            MaxStamina = maxStamina;
            CurrentStamina = maxStamina;
            RedKeys = 9;
            GreenKeys = 8;
            BlueKeys = 7;
        }
    }
}
