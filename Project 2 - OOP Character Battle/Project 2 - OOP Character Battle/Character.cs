using System;

namespace Project_2___OOP_Character_Battle {

    public abstract class Character {

        protected int moveSpeed;    //made protected so that child class can inherent but no one else can
        protected int damagePerAttack;
        protected int health;
        public int position;
        public int priority;
        protected int attackRange;

        public void TakeDamage(int amount) {
        }

        public string GetMovementDescription() {
            return "Movespeed: " + moveSpeed + "Attack Range: " + attackRange + " Damage: " + damagePerAttack;
        }

        public string Attack() {
            return "Attack was: ";
        }

        public abstract string Special(Character target);

        public abstract string GetSpecialDescription();




    }
}
