namespace Atomation.UnitTests;

using Atomation.Map;
using Atomation.Pawns;
using Atomation.Things;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

[TestSuite]
public class PlayerTest
{
    [TestCase]
    public void TestPlayerCreation()
    {
        Player player = AutoFree(Player.Instance);
        Player player2 = AutoFree(Player.Instance);

        AssertThat(player).IsEqual(player2);

        AssertThat(player.Name).IsEqual("player");

        AssertThat(player.GetStatSheet().GetStat(StatKeys.MOVE_SPEED).defName).IsEqual(StatKeys.MOVE_SPEED);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MOVE_SPEED).description).IsEqual("players moveSpeed");
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MOVE_SPEED).CurrentValue).IsEqual(1);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MOVE_SPEED).Type).IsEqual(StatType.Modifiable);

        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).defName).IsEqual(StatKeys.MAX_HEALTH);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).description).IsEqual("players hit points");
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(100);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).Type).IsEqual(StatType.Modifiable);

        AssertThat(player.GetStatSheet().GetStat(StatKeys.ATTACK_DAMAGE).defName).IsEqual(StatKeys.ATTACK_DAMAGE);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.ATTACK_DAMAGE).description).IsEqual("players Attack dmg");
        AssertThat(player.GetStatSheet().GetStat(StatKeys.ATTACK_DAMAGE).CurrentValue).IsEqual(10);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.ATTACK_DAMAGE).Type).IsEqual(StatType.Modifiable);
    }

    [TestCase]
    public void TestPlayerStatChange()
    {
        Player player = AutoFree(Player.Instance);

        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(100);

        player.Damage(10);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(90);

        player.Heal(10);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(100);

        player.Damage(100);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(0);
    }
}