using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2___OOP_Character_Battle
{
    class Warrior : Character
    {
        public Warrior()
        {
            this.moveSpeed = 2;
            this.damagePerAttack = 20;
            this.health = 75;
            this.priority = 3;
            this.attackRange = 1;
        }

        public override string Special(Character target)
        {
            return target.GetSpecialDescription();
        }

        public override string GetSpecialDescription()
        {
            return "leap up to 8 units to the spot in front of the opponent if possible, if opponent is greater than 5 units away deal 30 damage";
        }
    }
}
