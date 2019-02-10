using System;

namespace Project_2___OOP_Character_Battle {

    public abstract class Character {

        public int moveSpeed;    
        public int damagePerAttack; //made protected so that child class can inherent but no one else can
        public int health;
        public int position;
        public int priority;
        public int attackRange;
        public string characterType;
        public int attackSpecialRange;
        public int damagePerSpecialAttack;

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
