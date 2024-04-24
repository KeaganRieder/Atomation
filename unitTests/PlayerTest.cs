namespace Atomation.UnitTests;

using Atomation.Map;
using Atomation.PlayerChar;
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
        Player player = AutoFree(new Player());

        AssertThat(player.Coordinate.WorldPosition).IsEqual(Vector2.Zero);
        AssertThat(player.Coordinate.ChunkGridPosition).IsEqual(Vector2.Zero);
        AssertThat(player.Coordinate.ChunkWorldPos).IsEqual(Vector2.Zero);
        AssertThat(player.Coordinate.XYPosition).IsEqual(Vector2I.Zero);

        player.SetSpawn(new Coordinate(new Vector2(-512, -512)));
        AssertThat(player.Coordinate.WorldPosition).IsEqual(new Vector2(-512, -512));
        AssertThat(player.Coordinate.ChunkGridPosition).IsEqual(new Vector2(-1, -1));
        AssertThat(player.Coordinate.ChunkWorldPos).IsEqual(new Vector2(-512, -512));
        AssertThat(player.Coordinate.XYPosition).IsEqual(new Vector2I(0, 0));

        AssertThat(player.Name).IsEqual("player");
        AssertThat(player.Description).IsEqual("The player Character");
    
        AssertThat(player.StatSheet.GetStat(StatKeys.MOVE_SPEED).Name).IsEqual(StatKeys.MOVE_SPEED);
        AssertThat(player.StatSheet.GetStat(StatKeys.MOVE_SPEED).Description).IsEqual("players moveSpeed");
        AssertThat(player.StatSheet.GetStat(StatKeys.MOVE_SPEED).CurrentValue).IsEqual(1);
        AssertThat(player.StatSheet.GetStat(StatKeys.MOVE_SPEED).Type).IsEqual(StatType.Modifiable);

        AssertThat(player.StatSheet.GetStat(StatKeys.MAX_HEALTH).Name).IsEqual(StatKeys.MAX_HEALTH);
        AssertThat(player.StatSheet.GetStat(StatKeys.MAX_HEALTH).Description).IsEqual("players hit points");
        AssertThat(player.StatSheet.GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(100);
        AssertThat(player.StatSheet.GetStat(StatKeys.MAX_HEALTH).Type).IsEqual(StatType.Modifiable);

        AssertThat(player.StatSheet.GetStat(StatKeys.ATTACK_DAMAGE).Name).IsEqual(StatKeys.ATTACK_DAMAGE);
        AssertThat(player.StatSheet.GetStat(StatKeys.ATTACK_DAMAGE).Description).IsEqual("players Attack dmg");
        AssertThat(player.StatSheet.GetStat(StatKeys.ATTACK_DAMAGE).CurrentValue).IsEqual(10);
        AssertThat(player.StatSheet.GetStat(StatKeys.ATTACK_DAMAGE).Type).IsEqual(StatType.Modifiable);
    }

    [TestCase]
    public void TestPlayerStatChange ()
    {
        Player player = AutoFree(new Player());

        AssertThat(player.StatSheet.GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(100);

        player.Damage(10);
        AssertThat(player.StatSheet.GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(90);

        player.Heal(10);
        AssertThat(player.StatSheet.GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(100);

        player.Damage(100);
        AssertThat(player.StatSheet.GetStat(StatKeys.MAX_HEALTH).CurrentValue).IsEqual(0);
    }




}

