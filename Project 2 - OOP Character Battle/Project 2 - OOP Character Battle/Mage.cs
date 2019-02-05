using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2___OOP_Character_Battle {

    class Mage : Character {

        public Mage() {
            this.moveSpeed = 1;
            this.damagePerAttack = 20;
            this.health = 50;
            this.priority = 2;
            this.attackRange = 6;
        }

        public override string Special(Character target) {
            return target.GetSpecialDescription();
        }

        public override string GetSpecialDescription() {
            return "knock back the opponent 4 units, range = 3, deals 3 damage";
        }



    }


    }
       
}
