using System;
using RpgSimulator.Domain.Entities;
using RpgSimulator.Domain.Exceptions;
using Xunit;

namespace RpgSimulator.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Player_ShouldStartWithCorrectHealth()
        {
            var player = new Player("TestHero", 15);
            Assert.Equal(100, player.Health);
            Assert.Equal(100, player.MaxHealth);
            Assert.True(player.IsAlive);
        }

        [Fact]
        public void Player_TakeDamage_ReducesHealth()
        {
            var player = new Player("TestHero", 15);
            player.TakeDamage(30);
            Assert.Equal(70, player.Health);
        }

        [Fact]
        public void Player_ShouldNotGoBelowZeroHealth()
        {
            var player = new Player("TestHero", 15);
            player.TakeDamage(200);
            Assert.Equal(0, player.Health);
            Assert.False(player.IsAlive);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        [InlineData(-5)]
        public void Player_CannotHaveInvalidStrength(int invalidStrength)
        {
            Assert.Throws<ArgumentException>(() => new Player("Test", invalidStrength));
        }

        [Fact]
        public void Player_AddExperience_LevelsUp()
        {
            var player = new Player("TestHero", 15);
            player.AddExperience(100);
            Assert.Equal(2, player.Level);
        }

        [Fact]
        public void Enemy_Factory_CreatesCorrectType()
        {
            var goblin = Enemy.Factory.CreateGoblin();
            Assert.Equal("Гоблін", goblin.Name);
            Assert.Equal(30, goblin.MaxHealth);
            Assert.Equal(50, goblin.ExperienceReward);
        }
    }
}