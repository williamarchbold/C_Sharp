using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2___OOP_Character_Battle
{
    class Archer : Character
    {
        public Archer()
        {
            this.moveSpeed = 3;
            this.damagePerAttack = 15;
            this.health = 65;
            this.priority = 3;
            this.attackRange = 3;
        }

        public override string Special(Character target)
        {
            return target.GetSpecialDescription();
        }

        public override string GetSpecialDescription()
        {
            return "12 range attack, deals 10 damage";
        }

    }
}
