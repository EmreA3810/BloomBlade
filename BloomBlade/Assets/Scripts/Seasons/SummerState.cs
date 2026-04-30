public class SummerState : ISeasonState
{
    private WeaponController weapon;
    public SummerState(WeaponController weapon) { this.weapon = weapon; }

    public void Attack() => weapon.springAttack.AttackWithStats(35, 0.9f); // 2 hit civarý
    public string GetName() => "Yaz";
}