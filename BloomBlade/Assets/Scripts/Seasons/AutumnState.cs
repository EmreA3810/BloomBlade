public class AutumnState : ISeasonState
{
    private WeaponController weapon;
    public AutumnState(WeaponController weapon) { this.weapon = weapon; }

    public void Attack() => weapon.springAttack.AttackWithStats(22, 1.5f); // 3 hit civarý
    public string GetName() => "Sonbahar";
}