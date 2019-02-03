using System;

namespace Project_2___OOP_Character_Battle {

    public class Character {

        private int moveSpeed;
        private int damagePerAttack;
        private int health;
        private int position;
        private int priority;
        private int attackRange;

        public void TakeDamage(int amount);

        public string GetMovementDescription() {
            return ToString("Movespeed: {0} Attack Range: {1}  Damage: {2}", moveSpeed, attackRange, damagePerAttack);
        }

        abstract string GetSpecialDescription() {
            return ToString("Special attack description: ");
        }

        public string Attack() {
            return "Attack was: ";
        }

        abstract string Special(Character target) {
            return "Here's what happened...";
        }




    }
}
