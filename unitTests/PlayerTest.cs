namespace Atomation.UnitTests;

using Atomation.Map;
using Atomation.Player;
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
        PlayerChar player = AutoFree(PlayerChar.GetInstance());
        PlayerChar player2 = AutoFree(PlayerChar.GetInstance());

        AssertThat(player).IsEqual(player2);

        AssertThat(player.GetCoordinate().GetWorldPosition()).IsEqual(Vector2.Zero);
        AssertThat(player.GetCoordinate().GetXYPosition()).IsEqual(Vector2I.Zero);
        AssertThat(player.GetCoordinate().ToChunkCords().GetWorldPosition()).IsEqual(Vector2.Zero);
        AssertThat(player.GetCoordinate().ToChunkCords().GetXYPosition()).IsEqual(Vector2I.Zero);

        player.SetPosition(new Coordinate(new Vector2(-512, -512)));
        AssertThat(player.GetCoordinate().GetWorldPosition()).IsEqual(new Vector2(-512, -512));
        AssertThat(player.GetCoordinate().GetXYPosition()).IsEqual(new Vector2I(0, 0));
        AssertThat(player.GetCoordinate().ToChunkCords().GetWorldPosition()).IsEqual(new Vector2(-512, -512));
        AssertThat(player.GetCoordinate().ToChunkCords().GetXYPosition()).IsEqual(new Vector2I(-1, -1));

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
        PlayerChar player = AutoFree(PlayerChar.GetInstance());

        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(100);

        player.Damage(10);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(90);

        player.Heal(10);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(100);

        player.Damage(100);
        AssertThat(player.GetStatSheet().GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(0);
    }
}