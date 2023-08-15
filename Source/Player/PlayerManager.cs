using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK 
{
    /// <summary>
    /// プレイヤーのマネージャー
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        // 選択しているキャラクター
        private Character character;

        // マネージャー
        private CharacterManager characterManager;
        private RelicManager relicManager;
        private CardManager cardManager;

        private PlayerStatsUI playerStatsUI;
        private Fighter player;

        private void Awake() 
        {
            characterManager = FindObjectOfType<CharacterManager>();
            relicManager = FindObjectOfType<RelicManager>();
            cardManager = FindObjectOfType<CardManager>();
            playerStatsUI = FindObjectOfType<PlayerStatsUI>();
            player = GetComponent<Fighter>();
        }

        public void GameStart() 
        {
            characterManager.SetSelectedCharacter(Character.CharacterType.Warrior);
            character = characterManager.SelectedCharacter;
            relicManager.Initialization();
            relicManager.SetStartingRelic(character);
            playerStatsUI.Initialization();
            playerStatsUI.DisplayRelics(relicManager.PlayerRelic[0]);
            cardManager.Initialization();
            cardManager.SetPlayerDeck(character);
        }

        public List<Card> GetPlayerDeck() 
        {
            return cardManager.playerDeck;
        }

        public int GetMaxHealth() 
        {
            return character.MaxHealth;
        }

        public int GetMaxEnergy() 
        {
            return character.MaxEnergy;
        }

        public int GetDrawAmount() 
        {
            return character.DrawAmount;
        }

        public string GetCharacterType() 
        {
            if (character.Type == Character.CharacterType.Warrior) return "戦士";
            else return "不明"; 
        }

        public Sprite GetCharacterVisual() 
        {
            return character.Visual;
        }

        public bool HasRelics(string title) 
        {
            for (int i = 0; i < relicManager.PlayerRelic.Count; i++) 
            {
                if (relicManager.PlayerRelic[i].Title == title) 
                    return true;
            }
            return false;
        }

        public void RemoveCardinPlayerDeck(Card card) 
        {
            cardManager.playerDeck.Remove(card);
        }

        public void SetCoins(int amount) 
        {
            playerStatsUI.SetCoins(amount);
        }

        public Card GetRandomCard(Card.CardType t) 
        {
            return cardManager.GetRandomCard(t);
        }

        public void CheckRelics(Relic r) 
        {
            if (r.Title == "黒い宝石" || r.Title == "黒いグローブ" || r.Title == "黒い盾")
                player.GetMaxHealth(-5);
                
            if (r.Title == "竜の石") 
                player.GetMaxHealth(30);
        }
    } 
}